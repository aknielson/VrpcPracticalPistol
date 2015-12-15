using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DomainClasses;

namespace OutOfTheBoxMvc.Models
{
    //This is necessary to get rid of time on the date display of matches.... stupid framework
    public class ChooseMatchVM
    {
        
        public ChooseMatchVM(List<Match> matches, string action, string controller)
        {
            this.TargetAction = action;
            this.TargetController = controller;
            Matches = matches;
        }

        public List<Match> Matches { get; set; }
        public string TargetAction { get; set; }
        public string TargetController { get; set; }

        public List<SelectListItem> MatchSelectListItems
        {
            get
            {
                var returnVal = new List<SelectListItem>();
                foreach (var match in Matches)
                {
                    returnVal.Add(new SelectListItem {Text = match.Date.ToShortDateString(), Value = match.Id.ToString()});
                }
                return returnVal;
            }
        }
    }
}