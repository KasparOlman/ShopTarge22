﻿using System.ComponentModel.DataAnnotations;

namespace ShopTARge22.Models.Forecast
{
    public class SearchCityViewModel
    {
        [Required(ErrorMessage = "You must enter a city name.")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage ="Only text is allowed.")]
        [StringLength(20,MinimumLength =2, ErrorMessage = "Enter a city name what is greater than 2 and lesser than 20 letters")]
        [Display(Name ="City Name")]
        public string CityName { get; set; }
    }
}
