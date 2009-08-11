using System;
using System.Xml;
using System.Collections;
using System.ComponentModel;
using HHOnline.Common;
using HHOnline.Cache;

namespace HHOnline.Framework
{
    public delegate void UserEventHandler(User user, HHEventArgs e);

    public class GlobalApplication : BaseApplication
    {
        #region --Constructor--
        private GlobalApplication()
        {
            XmlNode sectionNode = HHConfiguration.GetConfig().GetConfigSection("HHOnline/GlobalApplication");
            LoadModules(sectionNode);
        }

        public static GlobalApplication Instance
        {
            get
            {
                GlobalApplication app = HHCache.Instance.Get(CacheKeyManager.GlobalApplicationKey) as GlobalApplication;
                if (app != null)
                    return app;

                lock (_mutex)
                {
                    app = HHCache.Instance.Get(CacheKeyManager.GlobalApplicationKey) as GlobalApplication;

                    if (app != null)
                        return app;

                    app = new GlobalApplication();
                }
                return app;
            }
        }

        public override object InitModule(Type moduleType, XmlNode node)
        {
            var module = Activator.CreateInstance(moduleType) as IGlobalModule;
            if (module != null)
                module.Init(this, node);
            return module;
        }

        #endregion

        #region-- EventKey--
        private static object EventPreUserUpdate = new object();
        private static object EventPostUserUpdate = new object();
        private static object EventUserRemove = new object();
        private static object EventUserKnown = new object();
        private static object EventUserValidated = new object();
        #endregion

        #region --User Event--
        /// <summary>
        /// 用户通过验证事件
        /// </summary>
        public event UserEventHandler UserValidated
        {
            add { _events.AddHandler(EventUserValidated, value); }
            remove { _events.RemoveHandler(EventUserValidated, value); }
        }

        /// <summary>
        /// 用户更新前事件
        /// </summary>
        public event UserEventHandler PreUserUpdate
        {
            add { _events.AddHandler(EventPreUserUpdate, value); }
            remove { _events.RemoveHandler(EventPreUserUpdate, value); }
        }

        /// <summary>
        /// 用户更新后事件
        /// </summary>
        public event UserEventHandler PostUserUpdate
        {
            add { _events.AddHandler(EventPostUserUpdate, value); }
            remove { _events.RemoveHandler(EventPostUserUpdate, value); }
        }


        internal void ExecutePreUserUpdate(User user, ObjectState state)
        {
            ExecuteUserEvent(EventPreUserUpdate, user, state);
        }

        internal void ExecutePostUserUpdate(User user, ObjectState state)
        {
            ExecuteUserEvent(EventPostUserUpdate, user, state);
        }

        internal void ExecuteUserValidated(User user)
        {
            ExecuteUserEvent(EventUserValidated, user);
        }

        protected void ExecuteUserEvent(object EventKey, User user)
        {
            ExecuteUserEvent(EventKey, user, ObjectState.None);
        }

        protected void ExecuteUserEvent(object EventKey, User user, ObjectState state)
        {
            UserEventHandler handler = _events[EventKey] as UserEventHandler;
            if (handler != null)
            {
                handler(user, new HHEventArgs(state));
            }
        }


        #endregion



    }
}
