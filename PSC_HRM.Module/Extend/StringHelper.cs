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
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;

namespace PSC_HRM.Module
{
    public static class StringHelper
    {
        /// <summary>
        /// Kiểm tra một chuỗi có phải là mẫu không?
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsTemplate(this string template)
        {
            if (!string.IsNullOrWhiteSpace(template)
                && template.Contains('{')
                && template.Contains('}'))
                return true;
            return false;
        }

        /// <summary>
        /// Lấy format của mẫu
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public static string GetTemplateFormat(this string template)
        {
            int start = template.IndexOf('{');
            int length = template.IndexOf('}') + 1 - start;
            return template.Substring(start, length);
        }

        /// <summary>
        /// Tạo mẫu
        /// </summary>
        /// <param name="template"></param>
        /// <param name="soThuTu"></param>
        /// <returns></returns>
        public static string CreateTemplate(this string template, int soThuTu)
        {
            string format = GetTemplateFormat(template);
            return template.Replace(format, soThuTu.ToString(format.RemoteFormatCharaters()));
        }

        /// <summary>
        /// Lấy số từ mẫu có sẵn
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public static string GetNumberFromTemplate(this string template)
        {
            string result = string.Empty;

            Regex regex = new Regex(@"\d+");
            Match match = regex.Match(template);
            if (match.Success)
            {
                result = match.Value;
            }
            return result;
        }

        /// <summary>
        /// Xóa ký tự {} của format
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string RemoteFormatCharaters(this string format)
        {
            return format.Replace("{", "").Replace("}", "");
        }


        /// <summary>
        /// Xử lý tập điều kiện
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string XuLyDieuKien(this string criteria, IObjectSpace obs, bool phanQuyen = false, params object[] args)
        {
            string newCriteria = "";
            CriteriaOperator co = CriteriaEditorHelper.GetCriteriaOperator(criteria, typeof(TapDieuKien.DieuKienTongHop), obs);
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
                newCriteria = BoPhan(((XPObjectSpace)obs).Session, newCriteria, phanQuyen);
                newCriteria = newCriteria.Replace("AND", "");
            }
            else
                newCriteria = BoPhan(((XPObjectSpace)obs).Session, newCriteria, phanQuyen);

            newCriteria = newCriteria.Replace("({", "'").Replace("})#", "'");

            // =, >, <, >=, <=, <> operator
            string result = CalculatorOperator(newCriteria);

            // in, not in operator
            result = InOperator(result);

            //Contains([HoTen], 'th') 
            result = Contains(result);

            //Between(1, 10)
            result = Between(result);

            //Like
            result = Like(result);

            //StartsWith([HoTen], 'th') 
            result = StartsWith(result);

            //EndsWith([HoTen], 'th') 
            result = EndsWith(result);

            //enum
            result = EnumOperator(result);

            //tạo bảng
            if (!result.Contains("SoHieuCongChuc"))
            {
                if(!result.Contains("NgachLuong"))              
                    result = CreateTable(result);               
                else
                    result = CreateTableNew(result);
            }
            result = Common(result);

            return result;
        }

        public static string CreateTable(string result)
        {
            result = Regex.Replace(result, "\\w+([.]\\w+){2,10}", match =>
            {
                var count = (from c in match.Value.ToCharArray()
                             where c == '.'
                             select c).Count();

                if (count >= 2)
                {
                    int i = 0;
                    int length = match.Length;
                    int index = length;
                    while (index > 0)
                    {
                        index--;
                        if (match.Value[index] == '.')
                        {
                            i++;
                            if (i == 2)
                            {
                                return String.Format("{0}", match.Value.Substring(index + 1, length - index - 1));
                            }
                        }
                    }
                }
                return match.Value;
            });
            return result;
        }

        public static string CreateTableNew(string result)
        {
            result = Regex.Replace(result, "\\w+([.][a-zA-Z_]+){2,10}", match =>
            {
                var count = (from c in match.Value.ToCharArray()
                             where c == '.'
                             select c).Count();

                if (count >= 2)
                {
                    int i = 0;
                    int length = match.Length;
                    int index = length;
                    while (index > 0)
                    {
                        index--;
                        if (match.Value[index] == '.')
                        {
                            i++;
                            if (i == 2)
                            {
                                return String.Format("{0}", match.Value.Substring(index + 1, length - index - 1));
                            }
                        }
                    }
                }
                return match.Value;
            });
            return result;
        }

        public static string EnumOperator(string result)
        {
            result = Regex.Replace(result, "##Enum#\\w+([.]\\w+)*,\\w+#", match =>
            {
                string temp = match.Value;
                Match match1 = Regex.Match(temp, "\\w+([.]\\w+)*,");
                if (match1.Success)
                {
                    Assembly asm = Assembly.GetExecutingAssembly();
                    Type type = asm.GetType(match1.Value.Replace(",", ""));
                    if (type != null)
                    {
                        match1 = Regex.Match(temp, ",\\w+");
                        if (match1.Success)
                        {
                            object obj = Enum.Parse(type, match1.Value.Replace(",", ""));
                            if (obj != null)
                                return ((byte)obj).ToString();
                        }
                    }
                }
                return match.Value;
            });
            return result;
        }

        public static string EndsWith(string result)
        {
            result = Regex.Replace(result, "EndsWith[(]\\w+([.]\\w+)*, '[%]*(,*[.-<>=+]*\\w*)*[%]*'[)]", match =>
            {
                string temp = match.Value;

                StringBuilder column = new StringBuilder();
                Match match1 = Regex.Match(temp, "[(]\\w+([.]\\w+)*,");
                if (match1.Success)
                {
                    var count = (from c in match1.Value.ToCharArray()
                                 where c == '.'
                                 select c).Count();

                    if (count >= 2)
                    {
                        int i = 0;
                        int length = match1.Length;
                        int index = length;
                        while (index > 0)
                        {
                            index--;
                            if (match1.Value[index] == '.')
                            {
                                i++;
                                if (i == 2)
                                {
                                    column.Append(String.Format("{0}", match1.Value.Substring(index + 1, length - index - 2)));
                                    break;
                                }
                            }
                        }
                    }
                    else
                        column.Append(match1.Value.Substring(1, match1.Length - 2));
                }

                match1 = Regex.Match(temp, "'[%]*\\w*[%]*'");
                if (match1.Success)
                {
                    column.Append(String.Format(" Like N'%{0}'", match1.Value.Replace("'", "").Replace("%", "")));
                }

                return column.ToString();
            });
            return result;
        }

        public static string StartsWith(string result)
        {
            result = Regex.Replace(result, "StartsWith[(]\\w+([.]\\w+)*, '[%]*\\w*[%]*'[)]", match =>
            {
                string temp = match.Value;

                StringBuilder column = new StringBuilder();
                Match match1 = Regex.Match(temp, "[(]\\w+([.]\\w+)*,");
                if (match1.Success)
                {
                    var count = (from c in match1.Value.ToCharArray()
                                 where c == '.'
                                 select c).Count();

                    if (count >= 2)
                    {
                        int i = 0;
                        int length = match1.Length;
                        int index = length;
                        while (index > 0)
                        {
                            index--;
                            if (match1.Value[index] == '.')
                            {
                                i++;
                                if (i == 2)
                                {
                                    column.Append(String.Format("{0}", match1.Value.Substring(index + 1, length - index - 2)));
                                    break;
                                }
                            }
                        }
                    }
                    else
                        column.Append(match1.Value.Substring(1, match1.Length - 2));
                }

                match1 = Regex.Match(temp, "'[%]*\\w*[%]*'");
                if (match1.Success)
                {
                    column.Append(String.Format(" Like N'{0}%'", match1.Value.Replace("'", "").Replace("%", "")));
                }

                return column.ToString();
            });

            return result;
        }

        public static string Like(string result)
        {
            result = Regex.Replace(result, "Like '", match =>
            {
                return match.Value.Replace("'", "N'");
            });
            return result;
        }

        public static string Between(string result)
        {
            result = Regex.Replace(result, "Between[(][']?\\w*\\s*[']?, [']?\\w*\\s*[']?[)]", match =>
            {
                string temp = match.Value.Replace("(", " ").Replace(")", "").Replace(",", " AND");

                return temp;
            });
            result = Regex.Replace(result, "Between[(]#\\d{4}(-\\d{2}){2}#, #\\d{4}(-\\d{2}){2}#[)]", match =>
            {
                string temp = match.Value.Replace("(", " ").Replace(")", "").Replace(",", " AND").Replace("#", "'");

                return temp;
            });
            return result;
        }

        public static string Contains(string result)
        {
            result = Regex.Replace(result, "Contains[(]\\w+([.]\\w+)*, '[%]*\\w*[%]*'[)]", match =>
            {
                string temp = match.Value;

                StringBuilder column = new StringBuilder();
                Match match1 = Regex.Match(temp, "[(]\\w+([.]\\w+)*,");
                if (match1.Success)
                {
                    var count = (from c in match1.Value.ToCharArray()
                                 where c == '.'
                                 select c).Count();

                    if (count >= 2)
                    {
                        int i = 0;
                        int length = match1.Length;
                        int index = length;
                        while (index > 0)
                        {
                            index--;
                            if (match1.Value[index] == '.')
                            {
                                i++;
                                if (i == 2)
                                {
                                    column.Append(String.Format("dbo.{0}", match1.Value.Substring(index + 1, length - index - 2)));
                                    break;
                                }
                            }
                        }
                    }
                    else
                        column.Append(match1.Value.Substring(1, match1.Length - 2));
                }

                match1 = Regex.Match(temp, "'[%]*\\w*[%]*'");
                if (match1.Success)
                {
                    column.Append(String.Format(" Like N'%{0}%'", match1.Value.Replace("'", "").Replace("%", "")));
                }

                return column.ToString();
            });

            return result;
        }

        public static string CalculatorOperator(string newCriteria)
        {
            return Regex.Replace(newCriteria, "\\w+([.]\\w+)* [<>=]+ ##XpoObject#\\w+([.]\\w+)*", match =>
            {
                string temp = match.Value;
                StringBuilder table = new StringBuilder();
                if (temp.Contains("ChucVuKiemNhiem"))
                {
                    table.Append("ChucVuKiemNhiem.Oid");
                }
                else if (temp.Contains("CongViecDuocGiao"))
                {
                    table.Append("CongViecDuocGiao.Oid");
                }
                else if (temp.Contains("CongViecHienNay"))
                {
                    table.Append("CongViecHienNay.Oid");
                }
                else
                {
                    int begin = temp.LastIndexOf('.') + 1;
                    int end = temp.Length;
                    table.Append(String.Format("{0}.Oid", temp.Substring(begin, end - begin)));
                }
                Match match1 = Regex.Match(temp, " [<>=]+ ");
                if (match1.Success)
                    table.Append(match1.Value);

                return table.ToString();
            });
        }

        public static string BoPhan(Session session, string newCriteria, bool phanQuyen)
        {
            if (newCriteria.Contains("NhanVien.BoPhan"))
            {
                // in, not in bo phan operator
                newCriteria = Regex.Replace(newCriteria, "NhanVien.BoPhan In ([(]##XpoObject#PSC_HRM.Module.BaoMat.BoPhan[(][{]\\w{8}(-\\w{4}){3}-\\w{12}[}][)]#)+(, ##XpoObject#PSC_HRM.Module.BaoMat.BoPhan[(][{]\\w{8}(-\\w{4}){3}-\\w{12}[}][)]#)*[)]", match =>
                {
                    string temp = match.Value;
                    MatchCollection matches = Regex.Matches(temp, "\\w{8}(-\\w{4}){3}-\\w{12}");
                    List<string> oid = new List<string>();
                    if (matches.Count > 0)
                    {
                        Guid boPhanID;
                        BoPhan bp;

                        foreach (Match matche in matches)
                        {
                            if (Guid.TryParse(matche.Value, out boPhanID))
                            {
                                bp = session.GetObjectByKey<BoPhan>(boPhanID);
                                List<string> oidTemp = HamDungChung.DanhSachBoPhanDuocPhanQuyen(bp);
                                if (oidTemp.Count > 0)
                                    oid.AddRange(oidTemp);
                            }
                        }
                    }

                    StringBuilder sb = new StringBuilder("NhanVien.BoPhan In (");
                    bool state = false;
                    foreach (string item in oid)
                    {
                        if (state)
                            sb.Append(", ");
                        else
                            state = true;
                        //sb.Append("##XpoObject#PSC_HRM.Module.BaoMat.BoPhan({");
                        sb.Append("'" + item + "'");
                        //sb.Append("})#");
                    }
                    sb.Append(")");
                    return sb.ToString();
                });

                // =, <> bo phan operator
                newCriteria = Regex.Replace(newCriteria, "NhanVien.BoPhan [<>=]+ ##XpoObject#PSC_HRM.Module.BaoMat.BoPhan[(][{]\\w{8}(-\\w{4}){3}-\\w{12}[}][)]#", match =>
                {
                    string temp = match.Value;
                    Match match1 = Regex.Match(temp, "[<>=]+");
                    if (match1.Success)
                    {
                        StringBuilder sb;
                        if (match1.Value == "=")
                            sb = new StringBuilder("NhanVien.BoPhan In (");
                        else
                            sb = new StringBuilder("NhanVien.BoPhan Not In (");

                        Match matche = Regex.Match(temp, "\\w{8}(-\\w{4}){3}-\\w{12}");
                        List<string> oid = new List<string>();
                        if (matche.Success)
                        {
                            Guid boPhanID;
                            BoPhan bp;
                            if (Guid.TryParse(matche.Value, out boPhanID))
                            {
                                bp = session.GetObjectByKey<BoPhan>(boPhanID);
                                List<string> oidTemp = HamDungChung.DanhSachBoPhanDuocPhanQuyen(bp);
                                if (oidTemp.Count > 0)
                                    oid.AddRange(oidTemp);
                            }
                        }

                        bool state = false;
                        foreach (string item in oid)
                        {
                            if (state)
                                sb.Append(", ");
                            else
                                state = true;
                            //sb.Append("##XpoObject#PSC_HRM.Module.BaoMat.BoPhan({");
                            sb.Append("'" + item + "'");
                            //sb.Append("})#");
                        }
                        sb.Append(")");

                        return sb.ToString();
                    }

                    return temp;
                });
            }
            else if (phanQuyen)
            {
                StringBuilder sb = new StringBuilder(" AND NhanVien.BoPhan In (");
                //
                List<string> oidTempList  = HamDungChung.DanhSachBoPhanDuocPhanQuyen(session);
                //
                bool state = false;
                foreach (string item in oidTempList)
                {
                    if (state)
                        sb.Append(", ");
                    else
                        state = true;
                    //sb.Append("##XpoObject#PSC_HRM.Module.BaoMat.BoPhan({");
                    sb.Append("'" + item + "'");
                    //sb.Append("})#");
                }
                sb.Append(")");
                newCriteria += sb.ToString();
            }

            return newCriteria;
        }

        public static string InOperator(string result)
        {
            result = Regex.Replace(result, "\\w+([.]\\w+)* In [(]##XpoObject#", match =>
            {
                string temp = match.Value;
                int begin = temp.IndexOf('.');
                begin = begin < 0 ? 0 : begin;
                int end = temp.IndexOf(' ');
                StringBuilder table = new StringBuilder();
                table.Append(String.Format("{0}.Oid", temp.Substring(begin, end - begin).Replace(".", "")));

                Match match1 = Regex.Match(temp, " In [(]##XpoObject#");
                if (match1.Success)
                    table.Append(match1.Value);

                return table.ToString();
            });
            result = Regex.Replace(result, "##XpoObject#\\w+([.]\\w+)*", "");

            return result;
        }

        public static string Common(string result, params object[] args)
        {
            //ngày tháng
            result = Regex.Replace(result, "#\\d{4}(-\\d{2}){2}#", match =>
            {
                return match.Value.Replace("#", "'");
            });

            //Thâm niên làm việc
            result = Regex.Replace(result, "HoSo.ThamNienLamViec", match =>
            {
                return String.Format("DateDiff(yy, dbo.NhanVien.NgayVaoNganhGiaoDuc, '{0:yyyy-MM-dd}')", args[1]);
            });

            //Số năm công tác tại trường
            result = Regex.Replace(result, "HoSo.SoNamCongTacTaiTruong", match =>
            {
                return String.Format("DateDiff(yy, dbo.NhanVien.NgayVaoCoQuan, '{0:yyyy-MM-dd}')", args[01]);
            });

            //Số tháng công tác tại trường
            result = Regex.Replace(result, "HoSo.SoThangCongTacTaiTruong", match =>
            {
                return String.Format("DateDiff(mm, dbo.NhanVien.NgayVaoCoQuan, '{0:yyyy-MM-dd}')", args[1]);
            });

            //Sinh nhật
            result = Regex.Replace(result, "HoSo.SinhNhat = True", match =>
            {
                return String.Format("Month('{0:yyyy-MM-dd}') = Month(dbo.HoSo.NgaySinh)", args[1]);
            });

            //Không phải sinh nhật
            result = Regex.Replace(result, "HoSo.SinhNhat = False", match =>
            {
                return String.Format("Month('{0:yyyy-MM-dd}') <> Month(dbo.HoSo.NgaySinh)", args[1]);
            });

            //Tuoi dang
            result = Regex.Replace(result, "DangVien.TuoiDang", match =>
            {
                return String.Format("DateDiff(yy, dbo.DangVien.NgayVaoDangChinhThuc, '{0:yyyy-MM-dd}')", args[1]);
            });

            //tuoi
            result = Regex.Replace(result, "HoSo.Tuoi", match =>
            {
                return String.Format("DateDiff(yy, dbo.HoSo.NgaySinh, '{0:yyyy-MM-dd}')", args[1]);
            });

            //Được khen thưởng
            result = Regex.Replace(result, "QuyetDinhKhenThuong.DuocKhenThuong = True", match =>
            {
                return String.Format("(Select Isnull((Select Count(*) From dbo.ChiTietKhenThuongNhanVien a Inner Join dbo.QuyetDinhKhenThuong b on a.QuyetDinhKhenThuong = b.Oid Inner Join dbo.QuyetDinh c on b.Oid = c.Oid Where c.GCRecord Is Null And a.GCRecord Is Null And a.ThongTinNhanVien = dbo.ThongTinNhanVien.Oid And c.NgayHieuLuc >= '{0:yyyy-MM-dd}' and c.NgayHieuLuc <= '{1:yyyy-MM-dd}'), 0)) > 0", args[0], args[1]);
            });

            //Không được khen thưởng
            result = Regex.Replace(result, "QuyetDinhKhenThuong.DuocKhenThuong = False", match =>
            {
                return String.Format("(Select Isnull((Select Count(*) From dbo.ChiTietKhenThuongNhanVien a Inner Join dbo.QuyetDinhKhenThuong b on a.QuyetDinhKhenThuong = b.Oid Inner Join dbo.QuyetDinh c on b.Oid = c.Oid Where c.GCRecord Is Null And a.GCRecord Is Null And a.ThongTinNhanVien = dbo.ThongTinNhanVien.Oid And c.NgayHieuLuc >= '{0:yyyy-MM-dd}' and c.NgayHieuLuc <= '{1:yyyy-MM-dd}'), 0)) = 0", args[0], args[1]);
            });

            //Bi ky luat
            result = Regex.Replace(result, "QuyetDinhKyLuat.BiKyLuat = True", match =>
            {
                return String.Format("(Select Isnull((Select Count(*) From dbo.QuyetDinhKyLuat a Inner Join dbo.QuyetDinhCaNhan b on a.Oid = b.Oid Inner Join dbo.QuyetDinh c on b.Oid = c.Oid Where c.GCRecord Is Null And b.ThongTinNhanVien = dbo.ThongTinNhanVien.Oid And c.NgayHieuLuc >= '{0:yyyy-MM-dd}' and c.NgayHieuLuc <= '{1:yyyy-MM-dd}'), 0)) > 0", args[0], args[1]);
            });

            //Khong bi ky luat
            result = Regex.Replace(result, "QuyetDinhKyLuat.BiKyLuat = False", match =>
            {
                return String.Format("(Select Isnull((Select Count(*) From dbo.QuyetDinhKyLuat a Inner Join dbo.QuyetDinhCaNhan b on a.Oid = b.Oid Inner Join dbo.QuyetDinh c on b.Oid = c.Oid Where c.GCRecord Is Null And b.ThongTinNhanVien = dbo.ThongTinNhanVien.Oid And c.NgayHieuLuc >= '{0:yyyy-MM-dd}' and c.NgayHieuLuc <= '{1:yyyy-MM-dd}'), 0)) = 0", args[0], args[1]);//se replace cai @[KyLuat]@ = NgayHieuLuc between ? and ? khi thuc thi
            });

            //mặc định nếu null thì cho ra true
            //Đánh giá cán bộ
            result = Regex.Replace(result, "(DanhGiaLan2.XepLoai = '[ABCD]')|(DanhGiaLan2.XepLoai <> '[ABCD]')|(DanhGiaLan2.XepLoai > '[ABCD]')|(DanhGiaLan2.XepLoai >= '[ABCD]')|(DanhGiaLan2.XepLoai < '[ABCD]')|(DanhGiaLan2.XepLoai <= '[ABCD]')", match =>
            {
                string temp = match.ToString().Replace("DanhGiaLan2", "a").Replace("'A'", "0").Replace("'B'", "1").Replace("'C'", "2").Replace("'D'", "3").Replace("<>", "=").Replace(">=", "<=").Replace("<=", ">=").Replace("=", "<>").Replace("<", ">").Replace(">", "<");
                return String.Format("(Select Isnull((Select Count(*) From dbo.XepLoaiLan2 a Inner Join dbo.XepLoaiLaoDong b On a.XepLoaiLaoDong = b.Oid Where a.GCRecord Is Null And a.ThongTinNhanVien = dbo.ThongTinNhanVien.Oid And {0} And b.ThangNam >= '{1:yyyy-MM-dd}' And b.ThangNam <= '{2:yyyy-MM-dd}'), 1)) = 0", temp, args[0], args[1]);//se replace cai @[KyLuat]@ = NgayHieuLuc between ? and ? khi thuc thi
            });

            //kieu so
            result = Regex.Replace(result, "\\d+([.]?\\d+)?m", match =>
            {
                return match.ToString().Replace("m", "");
            });

            //chuoi
            result = Regex.Replace(result, "[^N]'[%]?(\\w+\\s*[/]*)*[%]?'", match =>
            {
                return match.Value.Insert(1, "N");
            });

            //Bien che
            result = Regex.Replace(result, "True", match =>
            {
                return "1";
            });

            //Ngoai Bien che
            result = Regex.Replace(result, "False", match =>
            {
                return "0";
            });

            return result;
        }

        public static String ToTitleCase(String input)
        {
            String returnValue = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input);
            return returnValue;
        }
        public static String UpperFirstChar(String str)
        {
            StringBuilder builder = new StringBuilder();

            //bo di ky tu dau tien
            String partOfStr = str.Substring(1, str.Length - 1);
            //lay ky tu dau tien va chuyen thanh ky tu hoa
            String upperFirstChar = str.Substring(0, 1).ToUpper();

            return upperFirstChar + partOfStr;
        }
        public static String Replace(String s, Char[] replaceChars, Char replaceBy)
        {
            for (int i = 0; i < replaceChars.Length; i++)
            {
                s = s.Replace(replaceChars[i], replaceBy);
            }
            return s;
        }
        public static string ReplaceVietnameseChar(String s)
        {

            string[] vietnameseSigns = new string[]{
                "_aAeEoOuUiIdDyY",//replace char
                "áàạảãâấầậẩẫăắằặẳẵ",
                "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
                "éèẹẻẽêếềệểễ",
                "ÉÈẸẺẼÊẾỀỆỂỄ",
                "óòọỏõôốồộổỗơớờợởỡ",
                "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
                "úùụủũưứừựửữ",
                "ÚÙỤỦŨƯỨỪỰỬỮ",
                "íìịỉĩ",
                "ÍÌỊỈĨ",
                "đ",
                "Đ",
                "ýỳỵỷỹ",
                "ÝỲỴỶỸ"};
            //for (int i = 1; i < VietnameseSigns.Length; i++)
            //{
            //    for (int j = 0; j < VietnameseSigns[i].Length; j++)

            //        s = s.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            //}
            for (int i = 1; i < vietnameseSigns.Length; i++)
            {
                s = Replace(s, vietnameseSigns[i].ToCharArray(), vietnameseSigns[0][i]);
            }
            return s;
        }
    }
}
