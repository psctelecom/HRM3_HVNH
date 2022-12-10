using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// Set Time for DateTime value (use in Tax calculator)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="style">0: start, 1: end, 2: start month, 3: end month, 4: start year, 5: end year, 6: start week, 7: end week, 8: end work week</param>
        /// <returns></returns>
        public static DateTime SetTime(this DateTime source, SetTimeEnum type)
        {
            int hh, mm, ss;
            if (type == SetTimeEnum.StartDay)
            {
                hh = source.Hour;
                mm = source.Minute;
                ss = source.Second;

                source = source.AddHours(-hh);
                source = source.AddMinutes(-mm);
                source = source.AddSeconds(-ss);
            }
            else if (type == SetTimeEnum.EndDay)
            {
                hh = 23 - source.Hour;
                mm = 59 - source.Minute;
                ss = 59 - source.Second;

                source = source.AddHours(hh);
                source = source.AddMinutes(mm);
                source = source.AddSeconds(ss);
            }
            else if (type == SetTimeEnum.StartMonth)
            {
                source = new DateTime(source.Year, source.Month, 1);
                source = SetTime(source, SetTimeEnum.StartDay);
            }
            else if (type == SetTimeEnum.EndMonth)
            {
                source = new DateTime(source.Year, source.Month, 1).AddMonths(1).AddDays(-1);
                source = SetTime(source, SetTimeEnum.EndDay);
            }
            else if (type == SetTimeEnum.StartYear)
            {
                source = new DateTime(source.Year, 1, 1);
                source = SetTime(source, SetTimeEnum.StartDay);
            }
            else if (type == SetTimeEnum.EndYear)
            {
                source = new DateTime(source.Year, 12, 31);
                source = SetTime(source, SetTimeEnum.EndDay);
            }
            else if (type == SetTimeEnum.StartWeek)
            {
                DayOfWeek dayOfWeek = source.DayOfWeek;
                if (dayOfWeek != DayOfWeek.Sunday)
                    source = source.AddDays(-((int)dayOfWeek - 1));
                else
                    source = source.AddDays(-7);
                source = SetTime(source, SetTimeEnum.EndDay);
            }
            else if (type == SetTimeEnum.EndWeek)
            {
                DayOfWeek dayOfWeek = source.DayOfWeek;
                if (dayOfWeek != DayOfWeek.Sunday)
                    source = source.AddDays((7 - (int)dayOfWeek));
                source = SetTime(source, SetTimeEnum.EndDay);
            }
            else if (type == SetTimeEnum.EndWorkWeek)
            {
                DayOfWeek dayOfWeek = source.DayOfWeek;
                if (dayOfWeek != DayOfWeek.Sunday)
                    source = source.AddDays((5 - (int)dayOfWeek));
                source = SetTime(source, SetTimeEnum.EndDay);
            }

            return source;
        }

        /// <summary>
        /// là T7, CN và các ngày lễ
        /// </summary>
        /// <param name="source"></param>
        /// <param name="DenNgay"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public static bool IsHoliday(this DateTime source, Session session)
        {
            if (source.DayOfWeek == DayOfWeek.Saturday
                || source.DayOfWeek == DayOfWeek.Sunday)
                return true;

            using (XPCollection<NgayNghiTrongNam> list = new XPCollection<NgayNghiTrongNam>(session, CriteriaOperator.Parse("QuanLyNgayNghiTrongNam.Nam=?", source.Year)))
            {
                List<DateTime> NgayNghiTrongNamList = new List<DateTime>();
                foreach (NgayNghiTrongNam item in list)
                {
                    NgayNghiTrongNamList.Add(item.NgayNghi);
                }
            }
            return false;
        }

        /// <summary>
        /// Tính số ngày trong khoảng từ ngày, đến ngày, không tính T7, CN và các ngày lễ
        /// </summary>
        /// <param name="TuNgay"></param>
        /// <param name="DenNgay"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public static int TinhSoNgay(this DateTime TuNgay, DateTime DenNgay, Session session)
        {
            int SoNgay = 0;
            if (DenNgay.CompareTo(DateTime.MinValue) != 0 & TuNgay.CompareTo(DateTime.MinValue) != 0)
            {
                using (XPCollection<NgayNghiTrongNam> list = new XPCollection<NgayNghiTrongNam>(session, CriteriaOperator.Parse("QuanLyNgayNghiTrongNam.Nam=?", TuNgay.Year)))
                {
                    List<DateTime> NgayNghiTrongNamList = new List<DateTime>();
                    foreach (NgayNghiTrongNam item in list)
                    {
                        NgayNghiTrongNamList.Add(item.NgayNghi);
                    }
                    for (DateTime ngay = TuNgay; ngay <= DenNgay; ngay = ngay.AddDays(1))
                    {
                        if (ngay.DayOfWeek != DayOfWeek.Saturday & ngay.DayOfWeek != DayOfWeek.Sunday & !NgayNghiTrongNamList.Contains(ngay))
                        {
                            SoNgay++;
                        }
                    }
                }
            }
            return SoNgay;
        }

        /// <summary>
        /// Tính số ngày trong khoảng từ ngày, đến ngày, tính cả T7, CN và các ngày lễ
        /// </summary>
        /// <param name="TuNgay"></param>
        /// <param name="DenNgay"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public static int TinhSoNgay(this DateTime TuNgay, DateTime DenNgay)
        {
            int SoNgay = 0;
            if (DenNgay.CompareTo(DateTime.MinValue) != 0 & TuNgay.CompareTo(DateTime.MinValue) != 0)
            {
                SoNgay = DenNgay.Subtract(TuNgay).Days + 1;
            }
            return SoNgay;
        }

        /// <summary>
        /// Tinh so thang
        /// </summary>
        /// <param name="batDau">thang bat dau</param>
        /// <param name="ketThuc">thang ket thuc</param>
        /// <param name="session"></param>
        /// <returns></returns>
        public static int TinhSoThang(this DateTime batDau, DateTime ketThuc)
        {
            int count = 0;

            if (batDau.CompareTo(DateTime.MinValue) != 0 && ketThuc.CompareTo(DateTime.MinValue) != 0)
            {
                count = 12 * (ketThuc.Year - batDau.Year) + (ketThuc.Month - batDau.Month);
            }

            return Math.Abs(count);
        }

        /// <summary>
        /// Tính số năm
        /// </summary>
        /// <param name="batDau">thang bat dau</param>
        /// <param name="ketThuc">thang ket thuc</param>
        /// <returns></returns>
        public static int TinhSoNam(this DateTime batDau, DateTime ketThuc)
        {
            int count = 0;

            if (batDau.CompareTo(DateTime.MinValue) != 0 && ketThuc.CompareTo(DateTime.MinValue) != 0)
            {
                count = Math.Abs(ketThuc.Year - batDau.Year);
            }

            return count;
        }
    }
}
