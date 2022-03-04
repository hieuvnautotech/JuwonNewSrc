using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Helpers.Authentication
{
    public static class PermissionConstants
    {
        //CommonMaster
        public const string COMMONMASTER_CREATE = "Permission_createCommonMaster";
        public const string COMMONMASTER_MODIFY = "Permission_modifyCommonMaster";
        public const string COMMONMASTER_DELETE = "Permission_deleteCommonMaster";

        //CommonDetail
        public const string COMMONDETAIL_CREATE = "Permission_createCommonDetail";
        public const string COMMONDETAIL_MODIFY = "Permission_modifyCommonDetail";

        //Menu
        public const string MENU_CREATE = "Permission_createMenu";
        public const string MENU_MODIFY = "Permission_modifyMenu";

        //Permission
        public const string PERMISSION_CREATE = "Permission_createPermission";
        public const string PERMISSION_MODIFY = "Permission_modifyPermission";
        public const string PERMISSION_DELETE = "Permission_deletePermission";

        //Role
        public const string ROLE_CREATE = "Permission_createRole";
        public const string ROLE_MODIFY = "Permission_modifyRole";

        //UserInfo
        public const string USER_CREATE = "Permission_createUser";
        public const string USER_MODIFY = "Permission_modifyUser";

        //Model
        public const string MODEL_CREATE = "Permission_createModel";
        public const string MODEL_MODIFY = "Permission_modifyModel";

        //Product
        public const string PRODUCT_CREATE = "Permission_createProduct";
        public const string PRODUCT_MODIFY = "Permission_modifyProduct";

        //ModelStyle
        public const string MODELSTYLE_CREATE = "Permission_createModelStyle";
        public const string MODELSTYLE_MODIFY = "Permission_modifyModelStyle";

        //Buyer
        public const string BUYER_CREATE = "Permission_createBuyer";
        public const string BUYER_MODIFY = "Permission_modifyBuyer";
        public const string BUYER_DELETE = "Permission_deleteBuyer";

        //BuyerCategory
        public const string BUYERCATEGORY_CREATE = "Permission_createBuyerCategory";
        public const string BUYERCATEGORY_MODIFY = "Permission_modifyBuyerCategory";

        //Staff
        public const string STAFF_CREATE = "Permission_createStaff";
        public const string STAFF_MODIFY = "Permission_modifyStaff";
        public const string STAFF_DELETE = "Permission_deleteStaff";

        //Size
        public const string SIZE_CREATE = "Permission_createSize";
        public const string SIZE_MODIFY = "Permission_modifySize";

        //Box
        public const string BOX_CREATE = "Permission_createBox";
        public const string BOX_MODIFY = "Permission_modifyBox";

        //Department
        public const string DEPARTMENT_CREATE = "Permission_createDepartment";
        public const string DEPARTMENT_MODIFY = "Permission_modifyDepartment";
        public const string DEPARTMENT_DELETE = "Permission_deleteDepartment";

        //BaseColor
        public const string BASECOLOR_CREATE = "Permission_createBaseColor";
        public const string BASECOLOR_MODIFY = "Permission_modifyBaseColor";
        public const string BASECOLOR_DELETE = "Permission_deleteBaseColor";

        //SprayColor
        public const string SPRAYCOLOR_CREATE = "Permission_createSprayColor";
        public const string SPRAYCOLOR_MODIFY = "Permission_modifySprayColor";

        //StaffPosition
        public const string STAFFPOSITION_CREATE = "Permission_createStaffPosition";
        public const string STAFFPOSITION_MODIFY = "Permission_modifyStaffPosition";

        //BoxLabel
        public const string BOXLABEL_CREATE = "Permission_createBoxLabel";
        public const string BOXLABEL_MODIFY = "Permission_modifyBoxLabel";

        //BoxModel
        public const string BOXMODEL_CREATE = "Permission_createBoxModel";
        public const string BOXMODEL_MODIFY = "Permission_modifyBoxModel";

        //WO
        public const string WO_CREATE = "Permission_createWO";
        public const string WO_MODIFY = "Permission_modifyWO";
        public const string WO_DELETE = "Permission_deleteWO";

        //MenuUserGuide
        public const string MENUUSERGUIDE_CREATE = "Permission_createMenuUserGuide";
        public const string MENUUSERGUIDE_MODIFY = "Permission_modifyMenuUserGuide";

        //Location
        public const string LOCATION_CREATE = "Permission_createLocation";
        public const string LOCATION_MODIFY = "Permission_modifyLocation";
        public const string LOCATION_DELETE = "Permission_deleteLocation";

        //Destination
        public const string DESTINATION_CREATE = "Permission_createDestination";
        public const string DESTINATION_MODIFY = "Permission_modifyDestination";
        public const string DESTINATION_DELETE = "Permission_deleteDestination";

        //Factory
        public const string FACTORY_CREATE = "Permission_createFactory";
        public const string FACTORY_MODIFY = "Permission_modifyFactory";

        //LocationType
        public const string LOCATIONTYPE_CREATE = "Permission_createLocationType";
        public const string LOCATIONTYPE_MODIFY = "Permission_modifyLocationType";
        public const string LOCATIONTYPE_DELETE = "Permission_deleteLocationType";
        //Mold
        public const string MOLD_CREATE = "Permission_createLocationType";
        public const string MOLD_MODIFY = "Permission_modifyLocationType";
        public const string MOLD_DELETE = "Permission_deleteLocationType";

        //Part
        public const string PART_CREATE = "Permission_createPart";
        public const string PART_MODIFY = "Permission_modifyPart";
        public const string PART_DELETE = "Permission_deletePart";

        //QCMaster
        public const string QCMASTER_CREATE = "Permission_createQCMaster";
        public const string QCMASTER_MODIFY = "Permission_modifyQCMaster";
        public const string QCMASTER_DELETE = "Permission_deleteQCMaster";

        //QCDetail
        public const string QCDETAIL_CREATE = "Permission_createQCDetail";
        public const string QCDETAIL_MODIFY = "Permission_modifyQCDetail";
        public const string QCDETAIL_DELETE = "Permission_deleteQCDetail";

        //AreaCategory
        public const string AREACATEGORY_CREATE = "Permission_createAreaCategory";
        public const string AREACATEGORY_MODIFY = "Permission_modifyAreaCategory";
        public const string AREACATEGORY_DELETE = "Permission_deleteAreaCategory";

        //Area
        public const string AREA_CREATE = "Permission_createArea";
        public const string AREA_MODIFY = "Permission_modifyArea";
        public const string AREA_DELETE = "Permission_deleteArea";

        //PO
        public const string PO_CREATE = "Permission_createPO";
        public const string PO_MODIFY = "Permission_modifyPO";
        public const string PO_DELETE = "Permission_deletePO";

        //PODetail
        public const string PODETAIL_CREATE = "Permission_createPODetail";
        public const string PODETAIL_MODIFY = "Permission_modifyPODetail";
        public const string PODETAIL_DELETE = "Permission_deletePODetail";

        //POPartial
        public const string POPARTIAL_CREATE = "Permission_createPOPartial";
        public const string POPARTIAL_MODIFY = "Permission_modifyPOPartial";
        public const string POPARTIAL_DELETE = "Permission_deletePOPartial";

        //SO
        public const string SO_CREATE = "Permission_createSO";
        public const string SO_MODIFY = "Permission_modifySO";
        public const string SO_DELETE = "Permission_deleteSO";

        //Process
        public const string PROCESS_CREATE = "Permission_createProcess";
        public const string PROCESS_MODIFY = "Permission_modifyProcess";
        public const string PROCESS_DELETE = "Permission_deleteProcess";

        //Vendor
        public const string VENDOR_CREATE = "Permission_createVendor";
        public const string VENDOR_MODIFY = "Permission_modifyVendor";
        public const string VENDOR_DELETE = "Permission_deleteVendor";

        public const string VENDORCATEGORY_CREATE = "Permission_createVendorCategory";
        public const string VENDORCATEGORY_MODIFY = "Permission_modifyVendorCategory";
        public const string VENDORCATEGORY_DELETE = "Permission_deleteVendorCategory";

        //Supplier
        public const string SUPPLIER_CREATE = "Permission_createSupplier";
        public const string SUPPLIER_MODIFY = "Permission_modifySupplier";
        public const string SUPPLIER_DELETE = "Permission_deleteSupplier";

        //Season
        public const string SEASON_CREATE = "Permission_createSeason";
        public const string SEASON_MODIFY = "Permission_modifySeason";
        public const string SEASON_DELETE = "Permission_deleteSeason";

        //Recycle
        public const string RECYCLE_CREATE = "Permission_createRecycle";
        public const string RECYCLE_MODIFY = "Permission_deleteRecycle";
        public const string RECYCLE_DELETE = "Permission_modifyRecycle";

        //Material
        public const string MATERIAL_CREATE = "Permission_createMaterial";
        public const string MATERIAL_MODIFY = "Permission_modifyMaterial";
        public const string MATERIAL_DELETE = "Permission_deleteMaterial";

        //Bom
        public const string BOM_CREATE = "Permission_createBom";
        public const string BOM_MODIFY = "Permission_modifyBom";
        public const string BOM_DELETE = "Permission_deleteBom";

        //Bom Detail
        public const string BOMDETAIL_CREATE = "Permission_createBomDetail";
        public const string BOMDETAIL_MODIFY = "Permission_modifyBomDetail";
        public const string BOMDETAIL_DELETE = "Permission_deleteBomDetail";

    }
}
