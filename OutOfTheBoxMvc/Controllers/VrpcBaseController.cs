﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModel;

namespace OutOfTheBoxMvc.Controllers
{
    public class VrpcBaseController : Controller
    {
        protected VrpcPracticalPistolContext db = new VrpcPracticalPistolContext();

        protected List<SelectListItem> BuildMemberList()
        {
            var memberList = new List<SelectListItem>();
            memberList.Add(new SelectListItem
            {
                Value = "0",
                Text = "Member List"
            });


            foreach (var member in db.Members.OrderBy(x => x.FirstName))
            {
                memberList.Add(new SelectListItem
                {
                    Value = member.Id.ToString(),
                    Text = member.FirstName + ' ' + member.LastName
                });
            }
            return memberList;
        }
    }
}