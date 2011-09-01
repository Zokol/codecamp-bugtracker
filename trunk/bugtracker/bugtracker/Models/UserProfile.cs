using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.ComponentModel.DataAnnotations;

namespace bugtracker.Models
{
    public class UserProfile : ProfileBase
    {
        [Display(Name = "First Name")]
        public virtual string FirstName
        {
            get
            {
                return (this.GetPropertyValue("FirstName").ToString());
            }
            set
            {
                this.SetPropertyValue("FirstName", value);
            }
        }

        [Display(Name = "Last Name")]
        public virtual string LastName
        {
            get
            {
                return (this.GetPropertyValue("LastName").ToString());
            }
            set
            {
                this.SetPropertyValue("LastName", value);
            }
        }

        [Display(Name = "LastSignOff")]
        public virtual DateTime LastSignOff
        {
            get
            {
                return ((DateTime)this.GetPropertyValue("LastSignOff"));
            }
            set
            {
                this.SetPropertyValue("LastSignOff", value);
            }
        }

        [Display(Name = "LastNotificationCheck")]
        public virtual DateTime LastNotificationCheck
        {
            get
            {
                return ((DateTime)this.GetPropertyValue("LastNotificationCheck"));
            }
            set
            {
                this.SetPropertyValue("LastNotificationCheck", value);
            }
        }
        
        public static UserProfile GetProfile(string username)
        {
            return Create(username) as UserProfile;
        }
    }
}