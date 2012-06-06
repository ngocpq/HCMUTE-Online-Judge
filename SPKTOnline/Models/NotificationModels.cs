using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPKTOnline.BussinessLayer;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SPKTOnline.Models
{
    public class NotificationModels
    {
        [Required]
        [DisplayName("Tiêu đề")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Nội dung")]
        public string Body { get; set; }
    }
    public partial class Notification
    {
        INotificationBL notificationBL;
        OnlineSPKTEntities db = new OnlineSPKTEntities();
        public IEnumerable<Notification> OlderNoti
        {
            get {
                notificationBL = new NotificationBL(db);
                return notificationBL.GetOlderNoti(this.ID);
            }
        }
        public IEnumerable<Notification> LaterNoti
        {
            get
            {
                notificationBL = new NotificationBL(db);
                return notificationBL.GetLaterNoti(this.ID);
            }
        }
    }
}