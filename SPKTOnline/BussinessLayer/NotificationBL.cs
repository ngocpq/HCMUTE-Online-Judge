using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPKTOnline.Models;
using SPKTOnline.Management;

namespace SPKTOnline.BussinessLayer
{
    public interface INotificationBL
    {
        Notification GetNotificationByID(int NotiID);
        IEnumerable<Notification> GetAllNotification();
        IEnumerable<Notification> GetNotificationOfClass(int ClassID);
        IEnumerable<Notification> GetOlderNoti(int NotiID);
        IEnumerable<Notification> GetLaterNoti(int NotiID);
        bool SaveNotification(Notification noti);
    }
    public class NotificationBL: BusinessBase,INotificationBL
    {
        
        public NotificationBL(OnlineSPKTEntities db)
            : base(db)
        {
            
        }
        public IEnumerable<Notification> GetAllNotification()
        {
            return db.Notifications;
        }


        public IEnumerable<Notification> GetNotificationOfClass(int ClassID)
        {
            return db.Notifications.Where(n => n.ClassID == ClassID);
        }

        public bool SaveNotification(Notification noti)
        {
            try
            {
                BeginChange();
                noti.CreateDate = DateTime.Now;
                db.Notifications.AddObject(noti);
                CommitChange();
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.WriteLog(ex);
                RollbackChange();
                return false;
            }
        }


        public IEnumerable<Notification> GetOlderNoti(int NotiID)
        {
            Notification noti=GetNotificationByID(NotiID);
            return db.Notifications.Where(n => n.CreateDate < noti.CreateDate);
        }

        public IEnumerable<Notification> GetLaterNoti(int NotiID)
        {
            Notification noti = GetNotificationByID(NotiID);
            return db.Notifications.Where(n => n.CreateDate > noti.CreateDate);
        }

        public Notification GetNotificationByID(int NotiID)
        {
            return db.Notifications.FirstOrDefault(n => n.ID == NotiID);
        }
    }
}