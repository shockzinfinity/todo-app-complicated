using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace todoCore3.Api.Models.kte
{
    public class Dimensions
    {

        [JsonProperty("length")]
        public string length { get; set; }

        [JsonProperty("width")]
        public string width { get; set; }

        [JsonProperty("height")]
        public string height { get; set; }
    }

    public class Category
    {

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("slug")]
        public string slug { get; set; }
    }

    public class Tag
    {

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("slug")]
        public string slug { get; set; }
    }

    public class Image
    {

        [JsonProperty("id")]
        public int id { get; set; }

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

        [JsonProperty("src")]
        public string src { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("alt")]
        public string alt { get; set; }
    }

    public class wcProduct
    {

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("slug")]
        public string slug { get; set; }

        [JsonProperty("permalink")]
        public string permalink { get; set; }

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

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("status")]
        public string status { get; set; }

        [JsonProperty("featured")]
        public bool featured { get; set; }

        [JsonProperty("catalog_visibility")]
        public string catalog_visibility { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("short_description")]
        public string short_description { get; set; }

        [JsonProperty("sku")]
        public string sku { get; set; }

        [JsonProperty("price")]
        public string price { get; set; }

        [JsonProperty("regular_price")]
        public string regular_price { get; set; }

        [JsonProperty("sale_price")]
        public string sale_price { get; set; }

        [JsonProperty("date_on_sale_from")]
        public string date_on_sale_from { get; set; }

        [JsonProperty("date_on_sale_from_gmt")]
        public string date_on_sale_from_gmt { get; set; }

        [JsonProperty("date_on_sale_to")]
        public string date_on_sale_to { get; set; }

        [JsonProperty("date_on_sale_to_gmt")]
        public string date_on_sale_to_gmt { get; set; }

        [JsonProperty("price_html")]
        public string price_html { get; set; }

        [JsonProperty("on_sale")]
        public bool on_sale { get; set; }

        [JsonProperty("purchasable")]
        public bool purchasable { get; set; }

        [JsonProperty("total_sales")]
        public int total_sales { get; set; }

        [JsonProperty("virtual")]
        public bool isVirtual { get; set; }

        [JsonProperty("downloadable")]
        public bool downloadable { get; set; }

        [JsonProperty("downloads")]
        public IList<string> downloads { get; set; }

        [JsonProperty("download_limit")]
        public int download_limit { get; set; }

        [JsonProperty("download_expiry")]
        public int download_expiry { get; set; }

        [JsonProperty("external_url")]
        public string external_url { get; set; }

        [JsonProperty("button_text")]
        public string button_text { get; set; }

        [JsonProperty("tax_status")]
        public string tax_status { get; set; }

        [JsonProperty("tax_class")]
        public string tax_class { get; set; }

        [JsonProperty("manage_stock")]
        public bool manage_stock { get; set; }

        [JsonProperty("stock_quantity")]
        public int stock_quantity { get; set; }

        [JsonProperty("stock_status")]
        public string stock_status { get; set; }

        [JsonProperty("backorders")]
        public string backorders { get; set; }

        [JsonProperty("backorders_allowed")]
        public bool backorders_allowed { get; set; }

        [JsonProperty("backordered")]
        public bool backordered { get; set; }

        [JsonProperty("sold_individually")]
        public bool sold_individually { get; set; }

        [JsonProperty("weight")]
        public string weight { get; set; }

        [JsonProperty("dimensions")]
        public Dimensions dimensions { get; set; }

        [JsonProperty("shipping_required")]
        public bool shipping_required { get; set; }

        [JsonProperty("shipping_taxable")]
        public bool shipping_taxable { get; set; }

        [JsonProperty("shipping_class")]
        public string shipping_class { get; set; }

        [JsonProperty("shipping_class_id")]
        public int shipping_class_id { get; set; }

        [JsonProperty("reviews_allowed")]
        public bool reviews_allowed { get; set; }

        [JsonProperty("average_rating")]
        public string average_rating { get; set; }

        [JsonProperty("rating_count")]
        public int rating_count { get; set; }

        [JsonProperty("related_ids")]
        public IList<int> related_ids { get; set; }

        [JsonProperty("upsell_ids")]
        public IList<int> upsell_ids { get; set; }

        [JsonProperty("cross_sell_ids")]
        public IList<int> cross_sell_ids { get; set; }

        [JsonProperty("parent_id")]
        public int parent_id { get; set; }

        [JsonProperty("purchase_note")]
        public string purchase_note { get; set; }

        [JsonProperty("categories")]
        public IList<Category> categories { get; set; }

        [JsonProperty("tags")]
        public IList<Tag> tags { get; set; }

        [JsonProperty("images")]
        public IList<Image> images { get; set; }

        [JsonProperty("attributes")]
        public IList<string> attributes { get; set; }

        [JsonProperty("default_attributes")]
        public IList<string> default_attributes { get; set; }

        [JsonProperty("variations")]
        public IList<string> variations { get; set; }

        [JsonProperty("grouped_products")]
        public IList<string> grouped_products { get; set; }

        [JsonProperty("menu_order")]
        public int menu_order { get; set; }

        [JsonProperty("meta_data")]
        public IList<MetaData> meta_data { get; set; }

        [JsonProperty("_links")]
        public Links _links { get; set; }
    }


}
