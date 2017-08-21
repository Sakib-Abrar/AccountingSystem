using System;
using System.Collections.Generic;
using AccountingSystem.Controller;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Linq;

namespace AccountingSystem.Models
{
    class Login: INotifyPropertyChanged
    {
        public static DateTime? GlobalDate=null;
        private String m_cell = "";
        private String m_password = "";
        private String m_error_msg = " ";
        private DateTime? m_selectedDate=DateTime.Today;

        public DateTime? SelectedDate
        {
            get
            {
                return m_selectedDate;
            }
            set
            {
                m_selectedDate =value;
                GlobalDate = value;
            }
        }


        public string Cell
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

        #region Validation
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion


    }
}
