using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace todoCore3.Api.Models.kte
{
    public class OrderParam
    {
        //[JsonProperty("context", Required = Required.Default)]
        //[JsonIgnore]
        public string context { get; set; }
        //[JsonProperty("page", Required = Required.Default)]
        //[JsonIgnore]
        public int page { get; set; }
        //[JsonProperty("per_page", Required = Required.Default)]
        //[JsonIgnore]
        public int per_page { get; set; }
        //[JsonProperty("search", Required = Required.Default)]
        //[JsonIgnore]
        public string search { get; set; }
        //[JsonProperty("after", Required = Required.Default)]
        //[JsonIgnore]
        public DateTime? after { get; set; }
        //[JsonProperty("before", Required = Required.Default)]
        //[JsonIgnore]
        public DateTime? before { get; set; }
        //[JsonProperty("offset", Required = Required.Default)]
        //[JsonIgnore]
        public int offset { get; set; }
        //[JsonProperty("order", Required = Required.Default)]
        //[JsonIgnore]
        public string order { get; set; }
        //[JsonProperty("orderby", Required = Required.Default)]
        //[JsonIgnore]
        public string orderby { get; set; }
        //[JsonProperty("parent", Required = Required.Default)]
        //[JsonIgnore]
        public IList<string> parent { get; set; }
        //[JsonProperty("parent_exclude", Required = Required.Default)]
        //[JsonIgnore]
        public IList<string> parent_exclude { get; set; }
        //[JsonProperty("status", Required = Required.Default)]
        //[JsonIgnore]
        public IList<string> status { get; set; }
        //[JsonProperty("customer", Required = Required.Default)]
        //[JsonIgnore]
        public int customer { get; set; }
        //[JsonProperty("product", Required = Required.Default)]
        //[JsonIgnore]
        public int product { get; set; }
        //[JsonProperty("dp", Required = Required.Default)]
        //[JsonIgnore]
        public int dp { get; set; }

        public int currency { get; set; }
    }
}
