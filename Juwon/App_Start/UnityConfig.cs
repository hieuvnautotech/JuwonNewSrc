using Juwon.Repository;
using Juwon.Services.Interfaces;
using Juwon.Services.Implements;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace Juwon.App_Start
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            container.RegisterType<IRepository, RepositoryImplement>();
            container.RegisterType<IAppService, AppService>();
            container.RegisterType<ILoginService, LoginService>();
            container.RegisterType<IUserInfoService, UserInfoService>();
            container.RegisterType<IRoleService, RoleService>();
            container.RegisterType<IPermissionService, PermissionService>();
            container.RegisterType<IMenuService, MenuService>();
            container.RegisterType<ICommonMasterService, CommonMasterService>();
            container.RegisterType<ICommonDetailService, CommonDetailService>();
            container.RegisterType<IUserManualService, UserManualService>();

            container.RegisterType<IProcessService, ProcessService>();
            container.RegisterType<IColorService, ColorService>();
            container.RegisterType<IDestinationService, DestinationService>();
            container.RegisterType<IVendorCategoryService, VendorCategoryService>();
            container.RegisterType<IVendorService, VendorService>();
            container.RegisterType<IBuyerService, BuyerService>();
            container.RegisterType<ISupplierService, SupplierService>();
            container.RegisterType<IPartService, PartService>();
            container.RegisterType<IAreaCategoryService, AreaCategoryService>();
            container.RegisterType<IDepartmentService, DepartmentService>();
            container.RegisterType<IAreaService, AreaService>();
            container.RegisterType<ILocationCategoryService, LocationCategoryService>();
            container.RegisterType<IMoldService, MoldService>();
            container.RegisterType<ILocationService, LocationService>();
            container.RegisterType<IQCDetailService, QCDetailService>();
            container.RegisterType<IQCMasterService, QCMasterService>();
            container.RegisterType<IRecycleService, RecycleService>();
            container.RegisterType<ISeasonService, SeasonService>();
            container.RegisterType<IMaterialService, MaterialService>();
            container.RegisterType<IBomService, BomService>();
            container.RegisterType<IBomDetailService, BomDetailService>();
            container.RegisterType<IHieuService, HieuService>();
            container.RegisterType<IStaffHieuService, StaffHieuService>();


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}