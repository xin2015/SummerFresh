using SummerFresh.Business.Entity;
using SummerFresh.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace SummerFresh.Business.Service
{
    
    public class DepartmentEntityService:CustomEntityService
    {
        public override int Delete(object entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            string departmentId = string.Empty;
            if(entity is DepartmentEntity)
            {
                departmentId = (entity as DepartmentEntity).DepartmentId;
            }
            else
            {
                departmentId = entity.ToString();
            }
            var hasChildDepartment = Dao.Get().SelectAll<DepartmentEntity>().Where(o => o.ParentId == departmentId).Count() > 0;
            if (hasChildDepartment)
            {
                throw new Exception("组织内有子组织，不能删除！");
            }
            var hasUser = Dao.Get().SelectAll<UserEntity>().Where(o => o.DepartmentId.Equals(departmentId, StringComparison.CurrentCultureIgnoreCase)).Count() > 0;
            if (hasUser)
            {
                throw new Exception("组织内有用户，不能删除！");
            }
            using (TransactionScope tran = new TransactionScope())
            {

                //先删除 组织角色 的 用户
                new UserRoleEntityService().DeleteByDepartmentId(departmentId);

                //再删除 组织角色 的 权限
                new RoleEntityService().DeleteByDepartmentId(departmentId);

                //再删除 组织角色
                new RoleEntityService().DeleteByDepartmentId(departmentId);

                //再删除 组织
                int effectCount = base.Delete(entity);

                tran.Complete();

                return effectCount;
            }
        }

        public override IList<ButtonEntity> AddButtons()
        {
            var buttons = base.AddButtons();
            var button = buttons.FirstOrDefault(o => o.ButtonControlId == "btnBatchDelete");
            buttons.Remove(button);
            buttons.Add(new ButtonEntity() { 
                ButtonControlId="btnDepartmentUser",
                ButtonName = "组织人员",
                OnClick = "$.showModalDialog({{url:'Entity/List/User?DepartmentId={0}',width:900,height:500,overlayClose:true,title:'{1}的组织人员'}});",
                DataFields="DepartmentId,DepartmentName",
                ButtonType= ButtonEntityType.TableRow,
                CssClass = "btn btn-warning btn-sm"
            });
            buttons.Add(new ButtonEntity()
            {
                ButtonControlId = "btnDepartmentRole",
                ButtonName = "组织角色",
                OnClick = "$.showModalDialog({{url:'Entity/List/Role?DepartmentId={0}',width:900,height:500,overlayClose:true,title:'{1}的组织角色'}});",
                DataFields = "DepartmentId,DepartmentName",
                ButtonType = ButtonEntityType.TableRow,
                CssClass = "btn btn-warning btn-sm"
            });
            return buttons;
        }

        public override int Insert(SummerFresh.Business.Entity.CustomEntity entity)
        {
            DepartmentEntity ent = entity as DepartmentEntity;
            if (Dao.Get().SelectAll<DepartmentEntity>().Count(c => c.DepartmentName.Equals(ent.DepartmentName, StringComparison.InvariantCultureIgnoreCase) &&
                c.ParentId.Equals(ent.ParentId, StringComparison.InvariantCultureIgnoreCase) ) > 0)
            {
                throw new CustomException("组织已存在");
            }
            return base.Insert(entity);
        }

        public override int Update(CustomEntity entity)
        {
            DepartmentEntity ent=entity as DepartmentEntity;
            if (Dao.Get().SelectAll<DepartmentEntity>().Count(c=>c.DepartmentName.Equals(ent.DepartmentName,StringComparison.InvariantCultureIgnoreCase)&&
                c.ParentId.Equals(ent.ParentId,StringComparison.InvariantCultureIgnoreCase)&&
                !c.DepartmentId.Equals(ent.DepartmentId,StringComparison.InvariantCultureIgnoreCase)) > 0)
            {
                throw new CustomException("组织已存在");
            }
            return base.Update(entity);
        }
    }
}
