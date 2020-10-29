using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using todoCore3.Api.Models.kte;

namespace todoCore3.Api.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  [Produces("application/json")]
  public class wcOrderController : ControllerBase
  {
    private readonly AppSettings _appSettings;

    public wcOrderController(IOptions<AppSettings> appSettings)
    {
      _appSettings = appSettings.Value;
    }

    [HttpGet("orders")]
    public async Task<IActionResult> GetOrders(
      [FromQuery] DateTime? before,
      [FromQuery] DateTime? after,
      [FromQuery] string dateType,
      [FromQuery] string searchType,
      [FromQuery] string status,
      [FromQuery] string customerName,
      [FromQuery] int? partnerId,
      [FromQuery] int? orderNumber)
    {
      RestClient client = new RestClient(_appSettings.wcBaseApiUrl + "orders/");
      client.Authenticator = new HttpBasicAuthenticator(_appSettings.wooApiId, _appSettings.wooApiPw);
      RestRequest restRequest = new RestRequest();
      restRequest.Method = Method.GET;
      restRequest.AddHeader("Content-type", "application/json");
      restRequest.AddHeader("Accept", "application/json");
      restRequest.AddParameter("per_page", 100);
      // order date
      restRequest.AddParameter("before", before.HasValue ? before.Value.ToString("s") + "Z" : DateTime.Now.ToString("s") + "Z");
      restRequest.AddParameter("after", after.HasValue ? after.Value.ToString("s") + "Z" : DateTime.Now.ToString("s") + "Z");

      if (searchType == "reservation")
      { // 예약관리 화면
      }
      else if (searchType == "sales")
      { // 매출관리 화면
      }
      else
      { // 주문관리 화면
      }

      #region test
      //HttpClient httpClient = new HttpClient();
      //var json = JsonConvert.SerializeObject(new
      //{
      //  per_page = 100,
      //  before = before.HasValue ? before.Value.ToString("s") + "Z" : DateTime.Now.ToString("s") + "Z",
      //  after = after.HasValue ? after.Value.ToString("s") + "Z" : DateTime.Now.ToString("s") + "Z"
      //});

      //var httpRequest = new HttpRequestMessage
      //{
      //  Method = HttpMethod.Get,
      //  RequestUri = new Uri(_appSettings.wcBaseApiUrl + "orders/"),
      //  Content = new StringContent(json, Encoding.UTF8, "application/json")
      //};
      //var byteArray = Encoding.ASCII.GetBytes($"{_appSettings.wooApiId}:{_appSettings.wooApiPw}");
      //httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

      //HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequest);
      //HttpContent httpContent = httpResponse.Content;
      //string stringTemp = await httpContent.ReadAsStringAsync();

      //JArray temp2 = JArray.Parse(stringTemp);
      //List<OrderResponse> temp4 = new List<OrderResponse>();

      //foreach (JObject item in temp2)
      //{
      //  temp4.Add(ConvertOrder(item));
      //}

      #endregion


      IRestResponse restResponse = await client.ExecuteAsync(restRequest);

      JArray arrayTemp = JArray.Parse(restResponse.Content);
      List<OrderResponse> returnResponses = new List<OrderResponse>();

      foreach (JObject item in arrayTemp)
      {
        returnResponses.Add(ConvertOrder(item));
      }

      return Ok(Filter(returnResponses, customerName, status, orderNumber, partnerId));
    }

    private List<OrderResponse> Filter(List<OrderResponse> responses, string customerName, string status, int? orderNum, int? partnerId)
    {
      var results = new List<OrderResponse>();
      results.AddRange(responses);

      if (!string.IsNullOrEmpty(status))
      {
        results.RemoveAll(x => x.StatusOrig != status);
      }

      if (!string.IsNullOrEmpty(customerName))
      {
        results.RemoveAll(x => !x.CustomerName.Contains(customerName));
      }

      if (orderNum.HasValue)
      {
        results.RemoveAll(x => x.OrderId != orderNum.Value);
      }

      if (partnerId.HasValue)
      {
        results.RemoveAll(x => x.PartnerId != partnerId.Value.ToString());
      }

      return results;
    }

    [HttpGet("orders/{oid}")]
    public async Task<IActionResult> GetOrderBy(int oid)
    {
      RestClient client = new RestClient(_appSettings.wcBaseApiUrl + "orders/");

      client.Authenticator = new HttpBasicAuthenticator(_appSettings.wooApiId, _appSettings.wooApiPw);

      RestRequest restRequest = new RestRequest($"{oid}", Method.GET);

      IRestResponse restResponse = await client.ExecuteAsync(restRequest);
      JObject jObject = JObject.Parse(restResponse.Content);

      var response = ConvertOrder(jObject);

      return Ok(response);
    }

    private OrderResponse ConvertOrder(JObject jObject, int currency = 1150)
    {
      OrderResponse orderResponse = new OrderResponse();
      JArray itemMetas = null;
      JArray orderMetas = null;

      try
      {
        if (!jObject["line_items"].IsNullOrEmpty())
        {
          itemMetas = jObject["line_items"]?[0]?["meta_data"] as JArray;
        }

        if (!jObject["meta_data"].IsNullOrEmpty())
        {
          orderMetas = jObject["meta_data"] as JArray;
        }

        orderResponse.OrderId = (int)jObject["id"];
        orderResponse.OrderDate = jObject["date_created"].ToString();

        if (!string.IsNullOrEmpty(orderResponse.OrderDate))
        {
          orderResponse.OrderDate = DateTime.Parse(orderResponse.OrderDate).ToString("yyyy-MM-dd HH:mm:ss");
        }
        orderResponse.OrderTotal = jObject["total"].ToString();
        orderResponse.PaymentMethod = jObject["payment_method"].ToString();
        orderResponse.StatusOrig = jObject["status"].ToString();
        orderResponse.CustomerName = jObject["billing"]["first_name"].ToString() + " " + jObject["billing"]["last_name"].ToString();

        var region = jObject["billing"]["country"].ToString();
        try
        {
          RegionInfo regionInfo = new RegionInfo(region);

          orderResponse.National = regionInfo.EnglishName + " (" + regionInfo.Name + ")";
        }
        catch
        {
          orderResponse.National = "";
        }

        if (itemMetas != null)
        {
          orderResponse.ItemId = (int)jObject["line_items"][0]["id"];
          orderResponse.ProductName = jObject["line_items"][0]["name"].ToString();

          var attrs = jObject["line_items"][0]["meta_data"].Where(k => k["key"].ToString().StartsWith("attribute_")).ToList();
          foreach (var item in attrs)
          {
            orderResponse.OrderOptions += "<b>";

            switch (item["key"].ToString())
            {
              case "attribute_calendar_Date_Date_from":
                orderResponse.OrderOptions += "Date";
                break;
              case "attribute_number_Noofpeople_No. of people":
                orderResponse.OrderOptions += "No";
                break;
              case "attribute_selectbox_Touroption_Tour option":
                orderResponse.OrderOptions += "Tour option";
                break;
              case "attribute_quantity_Unit_car (basic)_Sedan (4pax)":
                orderResponse.OrderOptions += "car (basic)_Sedan (4pax)";
                break;
              case "attribute_selectbox_Itinerary_Itinerary":
                orderResponse.OrderOptions += "Itinerary";
                break;
              case "attribute_time_Meetingtime_Pickup time":
                orderResponse.OrderOptions += "Pickup time";
                break;
              case "attribute_text_Pickupaddress2_Pickup address":
                orderResponse.OrderOptions += "Pickup address";
                break;
              case "attribute_textarea_Specialrequest_Special request":
                orderResponse.OrderOptions += "Special request";
                break;
              default:
                orderResponse.OrderOptions += item["key"].ToString();
                break;
            }
            orderResponse.OrderOptions += "</b> : ";
            orderResponse.OrderOptions += item["value"].ToString();
            orderResponse.OrderOptions += "<br/>||";
          }

          orderResponse.Status = itemMetas.Where(k => (string)k["key"] == "_kte_item_label_text").FirstOrDefault()?["value"].ToString();
          orderResponse.StatusColor = itemMetas.Where(k => k["key"].ToString() == "_kte_item_label_color").FirstOrDefault()?["value"].ToString();

          orderResponse.NoOfPPL = itemMetas.Where(k => k["key"].ToString() == "_kte_item_num_of_ppl").FirstOrDefault()?["value"].ToString();
          //orderResponse.BookingDate = itemMetas?.Where(k => k["key"].ToString() == "attribute_calendar_Pickupdate_Pickup date_from").FirstOrDefault()?["value"].ToString();
          //orderResponse.ReturnDate = itemMetas?.Where(k => k["key"].ToString() == "attribute_calendar_Date_Return date_to").FirstOrDefault()?["value"].ToString();
          orderResponse.BookingDate = itemMetas.Where(k => k["key"].ToString() == "_use_date").FirstOrDefault()?["value"].ToString();
          orderResponse.ReturnDate = itemMetas.Where(k => k["key"].ToString() == "_end_date").FirstOrDefault()?["value"].ToString();
          orderResponse.PartnerBookingDate = itemMetas.Where(k => k["key"].ToString() == "_kte_item_partner_approve_date").FirstOrDefault()?["value"].ToString();
          orderResponse.PartnerId = itemMetas.Where(k => k["key"].ToString() == "_kp_vc_assigned_partner").FirstOrDefault()?["value"].ToString();

          var evidence = itemMetas.Where(k => k["key"].ToString() == "_kte_item_partner_evidence").FirstOrDefault()?["value"].ToString();
          switch (evidence)
          {
            case "tax_invoice": orderResponse.PartnerEvidence = "세금계산서"; break;
            case "cc": orderResponse.PartnerEvidence = "CC"; break;
            case "with_holding": orderResponse.PartnerEvidence = "원천징수"; break;
            case "en_invoice": orderResponse.PartnerEvidence = "인보이스"; break;
            default:
              orderResponse.PartnerEvidence = evidence;
              break;
          }
          orderResponse.PartnerAmount = itemMetas.Where(k => k["key"].ToString() == "_kte_item_partner_amount").FirstOrDefault()?.ToMetaValueString();
          orderResponse.PartnerApproveDate = itemMetas.Where(k => k["key"].ToString() == "_kte_item_partner_approve_date").FirstOrDefault()?["value"].ToString();
          orderResponse.PartnerPaymentMethod = itemMetas.Where(k => k["key"].ToString() == "_kte_item_partner_payment").FirstOrDefault()?["value"].ToString();
          orderResponse.PartnerPaidDate = itemMetas.Where(k => k["key"].ToString() == "_kte_item_partner_paid_date").FirstOrDefault()?["value"].ToString();
          orderResponse.IsItemUsed = itemMetas.Where(k => k["key"].ToString() == "_kte_item_used").FirstOrDefault()?["value"].ToString();

          orderResponse.ItemPrice = jObject["currency_symbol"].ToString() + " " + jObject["line_items"][0]["total"].ToString();
          orderResponse.ItemPriceDiscounted = "";
        }
        if (orderMetas != null)
        {
          orderResponse.PaymentDetail = orderMetas.Where(k => k["key"].ToString() == "_kte_order_payment_detail").FirstOrDefault()?["value"].ToString();
          orderResponse.PGDate = orderMetas.Where(k => k["key"].ToString() == "_kte_order_paid_date").FirstOrDefault()?["value"].ToString();
          orderResponse.PGAmount = orderMetas.Where(k => k["key"].ToString() == "_kte_order_revenue").FirstOrDefault()?["value"].ToString();
        }

        //(subtotal * 0.95 * currency) - _kte_item_partner_amount * 1
        //((subtotal * 0.95 * currency) - _kte_item_partner_amount * 1 )/ (total * currency) * 100
        //(total * 0.95 * currency) - _kte_item_partner_amount * 1
        //((total * 0.95 * currency) - _kte_item_partner_amount * 1 )/ (total * currency) * 100
        double subTotal = 0d;
        double total = 0d;

        if (itemMetas != null)
        {
          subTotal = double.Parse(jObject["line_items"][0]["subtotal"].ToString());
          total = double.Parse(jObject["line_items"][0]["total"].ToString());
        }
        double partnerAmount = 0d;
        double.TryParse(orderResponse.PartnerAmount, out partnerAmount);

        orderResponse.Margin = Convert.ToInt32(Math.Round((subTotal * 0.95 * currency) - partnerAmount)).ToString("N0");
        orderResponse.MarginPer = Math.Round(((subTotal * 0.95 * currency) - partnerAmount) / (total * currency) * 100, 2).ToString("N2") + " %";
        orderResponse.MarginReal = Convert.ToInt32(Math.Round(((total * 0.95 * currency) - partnerAmount))).ToString("N0");
        orderResponse.MarginRealPer = Math.Round(((total * 0.95 * currency) - partnerAmount) / (total * currency) * 100, 2).ToString("N2") + " %";
      }
      catch (Exception ex)
      {

      }

      return orderResponse;
    }

    [HttpGet]
    [Route("products")]
    public async Task<IActionResult> GetProducts()
    {
      RestClient client = new RestClient(_appSettings.wcBaseApiUrl + "products/");

      client.Authenticator = new HttpBasicAuthenticator(_appSettings.wooApiId, _appSettings.wooApiPw);

      RestRequest restRequest = new RestRequest();
      restRequest.Method = Method.GET;

      IRestResponse restResponse = await client.ExecuteAsync(restRequest);

      string tempStr = restResponse.Content;

      List<dynamic> temp = JsonConvert.DeserializeObject<List<dynamic>>(restResponse.Content);

      return Ok(temp);
    }

    [HttpGet]
    [Route("products/{pid}")]
    public async Task<IActionResult> GetProductBy(int pid)
    {
      RestClient client = new RestClient(_appSettings.wcBaseApiUrl + "products/");

      client.Authenticator = new HttpBasicAuthenticator(_appSettings.wooApiId, _appSettings.wooApiPw);

      RestRequest restRequest = new RestRequest($"{pid}", Method.GET);

      IRestResponse restResponse = await client.ExecuteAsync(restRequest);

      string tempStr = restResponse.Content;

      dynamic temp = JsonConvert.DeserializeObject<dynamic>(restResponse.Content);

      return Ok(temp);
    }
  }
}