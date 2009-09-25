using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace HHOnline.Framework
{
    public class CompanyTypeKeyValue
    {
        private static CompanyTypeKeyValue _Instance;
        public static CompanyTypeKeyValue Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new CompanyTypeKeyValue();
                }
                return _Instance;
            }
        }
        public List<ComTypeList> this[int index]
        {
            get
            {
                return (List<ComTypeList>)_arr[index];
            }
        }
        private object[] _arr = null;
        public CompanyTypeKeyValue()
        {
            BuildArray();
        }
        void BuildArray()
        {
            if (_arr != null) return;
            int length = (int)(CompanyType.Agent | CompanyType.Ordinary | CompanyType.Provider);
            _arr = new object[length + 1];

            List<ComTypeList> ctls = null;
            ComTypeList ctl = null;

            ctls = new List<ComTypeList>();
            ctl = new ComTypeList("agentType", "代理商");
            ctls.Add(ctl);
            _arr[(int)(CompanyType.Agent)] = ctls;

            ctls = new List<ComTypeList>();
            ctl = new ComTypeList("providerType", "供应商");
            ctls.Add(ctl);
            _arr[(int)(CompanyType.Provider)] = ctls;

            ctls = new List<ComTypeList>();
            ctl = new ComTypeList("originalType", "普通客户");
            ctls.Add(ctl);
            _arr[(int)(CompanyType.Ordinary)] = ctls;

            ctls = new List<ComTypeList>();
            ctl = new ComTypeList("originalType", "普通客户");
            ctls.Add(ctl);
            ctl = new ComTypeList("agentType", "代理商");
            ctls.Add(ctl);
            _arr[(int)(CompanyType.Agent | CompanyType.Ordinary)] = ctls;

            ctls = new List<ComTypeList>();
            ctl = new ComTypeList("originalType", "普通客户");
            ctls.Add(ctl);
            ctl = new ComTypeList("providerType", "供应商");
            ctls.Add(ctl);
            _arr[(int)(CompanyType.Ordinary | CompanyType.Provider)] = ctls;

            ctls = new List<ComTypeList>();
            ctl = new ComTypeList("agentType", "代理商");
            ctls.Add(ctl);
            ctl = new ComTypeList("providerType", "供应商");
            ctls.Add(ctl);
            _arr[(int)(CompanyType.Agent | CompanyType.Provider)] = ctls;

            ctls = new List<ComTypeList>();
            ctl = new ComTypeList("originalType", "普通客户");
            ctls.Add(ctl);
            ctl = new ComTypeList("agentType", "代理商");
            ctls.Add(ctl);
            ctl = new ComTypeList("providerType", "供应商");
            ctls.Add(ctl);
            _arr[(int)(CompanyType.Agent | CompanyType.Ordinary | CompanyType.Provider)] = ctls;
        }
    }

    public class ComTypeList
    {
        public ComTypeList() { }
        public ComTypeList(string cssClass, string title)
        {
            this._CssClass = cssClass;
            this._Title = title;
        }
        private string _CssClass;
        public string CssClass
        {
            get { return _CssClass; }
            set { _CssClass = value; }
        }

        private string _Title;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
    }
}
