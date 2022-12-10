using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using PSC_HRM.Module;
using PSC_HRM.Module.PMS.DieuKien;

namespace PSC_HRM.Module.PMS
{
    public static class PMSHelper
    {   

        /// <summary>
        /// Xử lý tập điều kiện
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string XuLyDieuKienPMS(this string criteria, IObjectSpace obs, bool phanQuyen = false, params object[] args)
        {
            string newCriteria = "";
            CriteriaOperator co = CriteriaEditorHelper.GetCriteriaOperator(criteria, typeof(DieuKien_ThuLaoGiangDay), obs);
            GroupOperator go = co as GroupOperator;
            if (!ReferenceEquals(go, null)
                && go.Operands.Count > 1
                && go.OperatorType == GroupOperatorType.Or)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in go.Operands)
                {
                    if (sb.Length > 0)
                        sb.Append(" or (" + item.ToString() + ")");
                    else
                        sb.Append("(" + item.ToString() + ")");
                }
                newCriteria = sb.ToString();
            }
            else
                newCriteria = co != null ? co.ToString() : string.Empty;
            //if (criteria.Contains("Or"))
            //{
            //    string[] split = criteria.Split(new string[] { " Or " }, StringSplitOptions.RemoveEmptyEntries);
            //    foreach (string item in split)
            //    {
            //        newCriteria += String.Format("({0}) Or ", item);
            //    }
            //    newCriteria = newCriteria.Remove(newCriteria.Length - 4);
            //    newCriteria = String.Format("({0})", newCriteria);
            //}
            //else
            //    newCriteria = criteria;
            newCriteria = newCriteria.Replace("[", "").Replace("]", "");

            //Bo phan
            if (newCriteria == string.Empty)
            {
                newCriteria = StringHelper.BoPhan(((XPObjectSpace)obs).Session, newCriteria, phanQuyen);
                newCriteria = newCriteria.Replace("AND", "");
            }
            else
                newCriteria = StringHelper.BoPhan(((XPObjectSpace)obs).Session, newCriteria, phanQuyen);

            newCriteria = newCriteria.Replace("({", "'").Replace("})#", "'");

            // =, >, <, >=, <=, <> operator
            string result = StringHelper.CalculatorOperator(newCriteria);

            // in, not in operator
            result = StringHelper.InOperator(result);

            //Contains([HoTen], 'th') 
            result = StringHelper.Contains(result);

            //Between(1, 10)
            result = StringHelper.Between(result);

            //Like
            result = StringHelper.Like(result);

            //StartsWith([HoTen], 'th') 
            result = StringHelper.StartsWith(result);

            //EndsWith([HoTen], 'th') 
            result = StringHelper.EndsWith(result);

            //enum
            result = StringHelper.EnumOperator(result);

            //tạo bảng
            if (!result.Contains("SoHieuCongChuc"))
            {
                if(!result.Contains("NgachLuong"))              
                    result = StringHelper.CreateTable(result);               
                else
                    result = StringHelper.CreateTableNew(result);
            }
            result = StringHelper.Common(result);

            return result;
        }     
    }
}
