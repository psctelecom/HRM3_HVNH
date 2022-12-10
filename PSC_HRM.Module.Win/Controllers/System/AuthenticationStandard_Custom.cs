using System;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp;
using PSC_HRM.Module.BaoMat;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using System.Data.SqlClient;
using DevExpress.Data.Filtering;
using System.Net.Sockets;
using System.Net;
using DevExpress.Persistent.Base.Security;

namespace PSC_HRM.Module.Win.Controllers
{
    public class AuthenticationStandard_Custom : AuthenticationStandard
    {
        public AuthenticationStandard_Custom(Type userType, Type logonParametersType) : base(userType, logonParametersType) { }

        public AuthenticationStandard_Custom()
        {
        }

        public override object Authenticate(IObjectSpace objectSpace)
        {
            NguoiSuDung loginUser;
            //
            AuthenticationStandardLogonParameters logonParameters = (AuthenticationStandardLogonParameters)LogonParameters;
            if (logonParameters == null) return null;
            //
            if (TruongConfig.MaTruong.Equals("QNU"))
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@UserName", logonParameters.UserName);

                object result1 = DataProvider.GetValueFromDatabase("spd_HeThong_KtraLoaiDangNhap", System.Data.CommandType.StoredProcedure, param);
                if (result1.ToString() == "SUCCESS")
                    //if (HamDungChung.CauHinhChung != null && HamDungChung.CauHinhChung.DongBoTaiKhoan)
                {
                    loginUser = CheckLogin_Other(objectSpace, logonParameters);
                }
                else
                {
                    loginUser = CheckLogin_ERP(objectSpace, logonParameters);
                }
                
            }
            else
            {
                #region Dùng 2 dòng này để test
                //    Session session = ((XPObjectSpace)objectSpace).Session;
                //    loginUser = session.FindObject<SecuritySystemUser_Custom>(CriteriaOperator.Parse("UserName = ? and IsActive = true", logonParameters.UserName));
                #endregion

                loginUser = CheckLogin_ERP(objectSpace, logonParameters);
            }
            //
            return loginUser;
        }

        private NguoiSuDung CheckLogin_ERP(IObjectSpace objectSpace, AuthenticationStandardLogonParameters logonParameters)
        {
            Session session = ((XPObjectSpace)objectSpace).Session;
            //
            NguoiSuDung currentUser = session.FindObject<NguoiSuDung>(CriteriaOperator.Parse("UserName = ? and IsActive = true", logonParameters.UserName));
            if (currentUser == null)
                throw new ArgumentException("Tên đăng nhập không đúng hoặc tài khoản đã bị khóa!");

            #region 1. Kiểm tra đăng nhập

            //Kiểm tra lỗi
            if (string.IsNullOrEmpty(logonParameters.UserName))
            {
                throw new ArgumentException("Tên đăng nhập không được rỗng!");
            }

            //1. Kiểm tra mật khẩu hiện tại
            if (!currentUser.ComparePassword(logonParameters.Password))
            {
                throw new ArgumentException("Tên đăng nhập hoặc mật khẩu không đúng!");
            }
            #endregion

            #region 2. Cập nhật thông tin
            //1. Cập nhật thông tin này quan trọng (dùng cho những lần đầu khi đăng nhập sẽ tự lấy mật khẩu)
            //Code mã hóa pw lưu db
            //currentUser.Password = logonParameters.Password;
            return currentUser;
            #endregion
        }

        private NguoiSuDung CheckLogin_Other(IObjectSpace objectSpace, AuthenticationStandardLogonParameters logonParameters)
        {
            NguoiSuDung currentUser = null;

            //Kiểm tra lỗi
            if (string.IsNullOrEmpty(logonParameters.UserName))
            {
                throw new ArgumentException("Tên đăng nhập không được rỗng!");
            }
            //
            Session session = ((XPObjectSpace)objectSpace).Session;


            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@UserName", logonParameters.UserName);
            param[1] = new SqlParameter("@PassWord", EncryptUtils.EncryptMD5(logonParameters.UserName, logonParameters.Password));

            object result = DataProvider.GetValueFromDatabase("spd_HeThong_CheckLoginURM", System.Data.CommandType.StoredProcedure, param);

            if (result.ToString() == "SUCCESS")
            {
                //
                currentUser = session.FindObject<NguoiSuDung>(CriteriaOperator.Parse("UserName = ?", logonParameters.UserName));
                if (currentUser == null)
                {
                    throw new ArgumentException("Tài khoản không tồn tại");
                }

                if (!currentUser.IsActive)
                {
                    throw new ArgumentException("Tài khoản đã bị khóa");
                }
            }
            else
            {
                throw new ArgumentException("Đăng nhập phân quyền tổng thất bại!" + Environment.NewLine + "{" + result.ToString() + "}");
            }

            return currentUser;
            //
        }
        //public class AllowCloneObject : 
    }
    public class AuthenticationStandard_Custom<UserType, LogonParametersType> : AuthenticationStandard_Custom
    {
        public AuthenticationStandard_Custom() : base(typeof(UserType), typeof(LogonParametersType)) { }
    }

    public class AuthenticationStandard_CustomWin<UserType> : AuthenticationStandard_Custom<UserType, AuthenticationStandardLogonParameters>
        where UserType : IAuthenticationStandardUser
    {

    }
}