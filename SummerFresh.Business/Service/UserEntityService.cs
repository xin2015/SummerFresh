using SummerFresh.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Business.Entity;
using System.Transactions;
namespace SummerFresh.Business.Service
{
    public class UserEntityService : CustomEntityService
    {
        public override int Insert(Entity.CustomEntity entity)
        {
            var user = entity as UserEntity;
            user.Password = SecurityContext.Provider.EncryptPassword("111");
            IList<IDictionary<string, object>> userList = (this.DataSource as EntityDataSource).TableSource.GetAll();
            if (userList.Count(c => c["LoginId"].Equals(user.LoginId) || c["UserCode"].Equals(user.UserCode)) > 0)
                throw new CustomException("用户编码或登录名已存在");
            
            return base.Insert(entity);
        }

        public override int Update(CustomEntity entity)
        {
            var user = entity as UserEntity;
            user.Password = SecurityContext.Provider.EncryptPassword(user.Password);

            IList<IDictionary<string, object>> userList = (this.DataSource as EntityDataSource).TableSource.GetAll();
            if (userList.Count(c => !c["UserId"].Equals(user.UserId) && (c["LoginId"].Equals(user.LoginId) || c["UserCode"].Equals(user.UserCode))) > 0)
                throw new CustomException("用户编码或登录名已存在");
            return base.Update(entity);
        }

        public override IList<ButtonEntity> AddButtons()
        {
            var buttons = base.AddButtons();
            buttons.Add(new ButtonEntity() { 
                ButtonControlId = "btnUserRole",
                CssClass = "btn btn-warning btn-sm", 
                ButtonName = "用户角色", 
                ButtonType = ButtonEntityType.TableRow,
                OnClick = "$.showModalDialog({{ url: '/Entity/List/UserRole?UserId={0}', overlayClose: true,title:'用户【{1}】的所有角色', width: 700, height: 500 }});" ,
                DataFields="UserId,UserName"
            });
            buttons.Add(new ButtonEntity() {
                ButtonControlId="btnResetPassword",
                CssClass = "btn btn-primary btn-sm",
                ButtonName="密码重置",
                ButtonType= ButtonEntityType.TableRow,
                OnClick = "summerFresh.dataService('/Home/ResetPassword',{{userId:'{0}'}},function(){{summerFresh.showSuccess('密码已重置，请通知相关用户及时修改密码','提示');}});",//"alert('密码已重置，请通知相关用户及时修改密码');",
                //OnClick = "summerFresh.dataService('/Home/ResetPassword',{{userId:'{0}'}},function(res){{if(res)summerFresh.showSuccess('密码已重置，请通知相关用户及时修改密码','提示');else summerFresh.showError('密码重置失败','提示');}})",//"alert('密码已重置，请通知相关用户及时修改密码');",
                DataFields="UserId",
            });
            return buttons;
        }

        public override int Delete(object entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            string userId = string.Empty;
            if (entity is UserEntity)
            {
                userId = (entity as UserEntity).UserId;
            }
            else
            {
                userId = entity.ToString();
            }
            using (TransactionScope tran = new TransactionScope())
            {
                //先删除 用户角色
                new UserRoleEntityService().DeleteByUserId(userId);

                //再删除 用户
                int effectCount = base.Delete(entity);

                tran.Complete();

                return effectCount;
            }
        }
    }
}
