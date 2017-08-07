using System;
using System.Collections.Generic;
using AccountingSystem.Controller;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Linq;

namespace AccountingSystem.Models
{
    class Login
    {
        private String m_cell = "";
        private String m_password = "";
        private String m_error_msg = " ";
        public Nullable<DateTime> SelectedDate { get; set; }


        public string sCell
        {
            get
            {
                return m_cell;
            }
            set
            {
                if (m_cell != value)
                {
                    m_cell = value;
                }

            }
        }


        public string Error_msg
        {
            get
            {
                return m_error_msg;
            }
            set
            {
                if (m_error_msg != value)
                {
                    m_error_msg = value;
                }

            }
        }




        public string Password
        {
            get
            {
                return m_password;
            }
            set
            {
                if (m_password != value)
                {
                    m_password = value;
                }

            }
        }



    }
}
