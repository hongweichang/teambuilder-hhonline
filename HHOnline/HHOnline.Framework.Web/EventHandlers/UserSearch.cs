using System;
using System.Xml;
using HHOnline.Framework;

namespace HHOnline.Framework.Web.EventHandlers
{
    public class UserSearch : IGlobalModule
    {
        public void Init(GlobalApplication context, XmlNode node)
        {
            context.UserSearchWord += new UserSearchEventHandler(context_UserSearchWord);
        }

        void context_UserSearchWord(string searchWord)
        {
            WordSearchManager.Insert(searchWord);
        }
    }
}
