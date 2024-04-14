using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CompanyManagement.Contracts.Models
{
    /// <summary>
    ///     Model for filtering data
    /// </summary>
    public class FilterModel
    {
        /// <summary>
        ///     The order of sort
        /// </summary>
        [JsonProperty("SortOrder")]
        public string? SortOrder { get; set; }

        /// <summary>
        ///     The field to sort by
        /// </summary>
        [JsonProperty("SortField")]
        public string? SortField { get; set; }

        /// <summary>
        ///     Number of the page to fetch
        /// </summary>
        [Range(1, double.PositiveInfinity)]
        [DefaultValue(1)]
        [JsonProperty("PageNumber")]
        public int PageNumber { get; set; } = 1;

        /// <summary>
        ///     Total items per page to fetch
        /// </summary>
        [DefaultValue(20)]
        [Range(1, double.PositiveInfinity)]
        [JsonProperty("PageSize")]
        public int PageSize { get; set; } = 20;
    }
}
