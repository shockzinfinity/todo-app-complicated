using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace todoCore3.Api.Models.kte
{
    public class Billing
    {
        [JsonProperty("first_name")]
        public string first_name { get; set; }

        [JsonProperty("last_name")]
        public string last_name { get; set; }

        [JsonProperty("company")]
        public string company { get; set; }

        [JsonProperty("address_1")]
        public string address_1 { get; set; }

        [JsonProperty("address_2")]
        public string address_2 { get; set; }

        [JsonProperty("city")]
        public string city { get; set; }

        [JsonProperty("state")]
        public string state { get; set; }

        [JsonProperty("postcode")]
        public string postcode { get; set; }

        [JsonProperty("country")]
        public string country { get; set; }

        [JsonProperty("email")]
        public string email { get; set; }

        [JsonProperty("phone")]
        public string phone { get; set; }
    }

    public class Shipping
    {

        [JsonProperty("first_name")]
        public string first_name { get; set; }

        [JsonProperty("last_name")]
        public string last_name { get; set; }

        [JsonProperty("company")]
        public string company { get; set; }

        [JsonProperty("address_1")]
        public string address_1 { get; set; }

        [JsonProperty("address_2")]
        public string address_2 { get; set; }

        [JsonProperty("city")]
        public string city { get; set; }

        [JsonProperty("state")]
        public string state { get; set; }

        [JsonProperty("postcode")]
        public string postcode { get; set; }

        [JsonProperty("country")]
        public string country { get; set; }
    }

    public class MetaData
    {

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("key")]
        public string key { get; set; }

        [JsonProperty("value")]
        public string value { get; set; }
    }

    public class LineItem
    {

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("product_id")]
        public int product_id { get; set; }

        [JsonProperty("variation_id")]
        public int variation_id { get; set; }

        [JsonProperty("quantity")]
        public int quantity { get; set; }

        [JsonProperty("tax_class")]
        public string tax_class { get; set; }

        [JsonProperty("subtotal")]
        public string subtotal { get; set; }

        [JsonProperty("subtotal_tax")]
        public string subtotal_tax { get; set; }

        [JsonProperty("total")]
        public string total { get; set; }

        [JsonProperty("total_tax")]
        public string total_tax { get; set; }

        [JsonProperty("taxes")]
        public IList<object> taxes { get; set; }

        [JsonProperty("meta_data")]
        public IList<MetaData> meta_data { get; set; }

        [JsonProperty("sku")]
        public string sku { get; set; }

        [JsonProperty("price")]
        public double price { get; set; }
    }

    public class Refund
    {

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("reason")]
        public string reason { get; set; }

        [JsonProperty("total")]
        public string total { get; set; }
    }

    public class Self
    {

        [JsonProperty("href")]
        public string href { get; set; }
    }

    public class Collection
    {

        [JsonProperty("href")]
        public string href { get; set; }
    }

    public class Customer
    {

        [JsonProperty("href")]
        public string href { get; set; }
    }

    public class Links
    {

        [JsonProperty("self")]
        public IList<Self> self { get; set; }

        [JsonProperty("collection")]
        public IList<Collection> collection { get; set; }

        [JsonProperty("customer")]
        public IList<Customer> customer { get; set; }
    }

    public class wcOrder
    {

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("parent_id")]
        public int parent_id { get; set; }

        [JsonProperty("number")]
        public string number { get; set; }

        [JsonProperty("order_key")]
        public string order_key { get; set; }

        [JsonProperty("created_via")]
        public string created_via { get; set; }

        [JsonProperty("version")]
        public string version { get; set; }

        [JsonProperty("status")]
        public string status { get; set; }

        [JsonProperty("currency")]
        public string currency { get; set; }

        [JsonProperty("date_created", Required = Required.Default)]
        [JsonIgnore]
        public DateTime date_created { get; set; }

        [JsonProperty("date_created_gmt", Required = Required.Default)]
        [JsonIgnore]
        public DateTime date_created_gmt { get; set; }

        [JsonProperty("date_modified", Required = Required.Default)]
        [JsonIgnore]
        public DateTime date_modified { get; set; }

        [JsonProperty("date_modified_gmt", Required = Required.Default)]
        [JsonIgnore]
        public DateTime date_modified_gmt { get; set; }

        [JsonProperty("discount_total")]
        public string discount_total { get; set; }

        [JsonProperty("discount_tax")]
        public string discount_tax { get; set; }

        [JsonProperty("shipping_total")]
        public string shipping_total { get; set; }

        [JsonProperty("shipping_tax")]
        public string shipping_tax { get; set; }

        [JsonProperty("cart_tax")]
        public string cart_tax { get; set; }

        [JsonProperty("total")]
        public string total { get; set; }

        [JsonProperty("total_tax")]
        public string total_tax { get; set; }

        [JsonProperty("prices_include_tax")]
        public bool prices_include_tax { get; set; }

        [JsonProperty("customer_id")]
        public int customer_id { get; set; }

        [JsonProperty("customer_ip_address")]
        public string customer_ip_address { get; set; }

        [JsonProperty("customer_user_agent")]
        public string customer_user_agent { get; set; }

        [JsonProperty("customer_note")]
        public string customer_note { get; set; }

        [JsonProperty("billing")]
        public Billing billing { get; set; }

        [JsonProperty("shipping")]
        public Shipping shipping { get; set; }

        [JsonProperty("payment_method")]
        public string payment_method { get; set; }

        [JsonProperty("payment_method_title")]
        public string payment_method_title { get; set; }

        [JsonProperty("transaction_id")]
        public string transaction_id { get; set; }

        [JsonProperty("date_paid", Required = Required.Default)]
        [JsonIgnore]
        public DateTime date_paid { get; set; }

        [JsonProperty("date_paid_gmt", Required = Required.Default)]
        [JsonIgnore]
        public DateTime date_paid_gmt { get; set; }

        [JsonProperty("date_completed")]
        public string date_completed { get; set; }

        [JsonProperty("date_completed_gmt")]
        public string date_completed_gmt { get; set; }

        [JsonProperty("cart_hash")]
        public string cart_hash { get; set; }

        [JsonProperty("meta_data")]
        public IList<MetaData> meta_data { get; set; }

        [JsonProperty("line_items")]
        public IList<LineItem> line_items { get; set; }

        [JsonProperty("tax_lines")]
        public IList<string> tax_lines { get; set; }

        [JsonProperty("shipping_lines")]
        public IList<string> shipping_lines { get; set; }

        [JsonProperty("fee_lines")]
        public IList<string> fee_lines { get; set; }

        [JsonProperty("coupon_lines")]
        public IList<string> coupon_lines { get; set; }

        [JsonProperty("refunds")]
        public IList<Refund> refunds { get; set; }

        [JsonProperty("currency_symbol")]
        public string currency_symbol { get; set; }

        [JsonProperty("_links")]
        public Links _links { get; set; }
    }

}
