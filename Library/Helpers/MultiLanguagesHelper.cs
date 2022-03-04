namespace Library.Helper
{
    public class StringDTO
    {
        public string label { get; set; }
    }

    #region UserModel
    public class _UserDTO
    {
        public StringDTO UserName { get; set; }
        public StringDTO Name { get; set; }
        public StringDTO Address { get; set; }
        public StringDTO Email { get; set; }
        public StringDTO Phone { get; set; }
    }
    #endregion

    #region CommonMasterModel
    public class _CommonMaster
    {
        public StringDTO Code { get; set; }
        public StringDTO Name { get; set; }
    }

    #endregion

    #region CommonDetailModel
    public class _CommonDetail
    {
        public StringDTO Code { get; set; }
        public StringDTO Name { get; set; }
        public StringDTO MasterName { get; set; }
        public StringDTO Active { get; set; }
    }
    #endregion

    #region PermissionModel
    public class _Permission
    {
        public StringDTO Name { get; set; }
        public StringDTO CategoryName { get; set; }
        public StringDTO Description { get; set; }
        public StringDTO Active { get; set; }
    }
    #endregion

    #region MenuModel
    public class _Menu
    {
        public StringDTO Code { get; set; }
        public StringDTO Name { get; set; }
        public StringDTO FullName { get; set; }
        public StringDTO CategoryName { get; set; }
        public StringDTO Link { get; set; }
        public StringDTO Active { get; set; }
    }
    #endregion

    #region RoleModel
    public class _Role
    {
        public StringDTO Name { get; set; }
        public StringDTO CategoryName { get; set; }
        public StringDTO Description { get; set; }
        public StringDTO Active { get; set; }
    }
    #endregion

    #region NameUserGuide
    public class NameUserGuide
    {

        public StringDTO Name { get; set; }

        public StringDTO Content { get; set; }
        public StringDTO MenuCode { get; set; }

        public StringDTO MenuLevel { get; set; }
        public StringDTO Language { get; set; }
        public StringDTO FullName { get; set; }

    }
    #endregion

    #region LineModel
    public class _Line
    {
        public StringDTO Name { get; set; }
        public StringDTO ProcessCategory { get; set; }
        public StringDTO Status { get; set; }
        public StringDTO Active { get; set; }

    }
    #endregion

    #region ProductModel
    public class _Product
    {
        public StringDTO Code { get; set; }
        public StringDTO Description { get; set; }
        public StringDTO ProductCategory { get; set; }
        public StringDTO CategoryName { get; set; }
        public StringDTO ProductType { get; set; }
        public StringDTO TypeName { get; set; }
        public StringDTO Active { get; set; }
        public StringDTO ApplyDate { get; set; }
        public StringDTO ApplyDateToString { get; set; }
        public StringDTO CreatedBy { get; set; }
        public StringDTO CreatedDate { get; set; }
        public StringDTO CreatedDateToString { get; set; }
        public StringDTO ModifiedBy { get; set; }
        public StringDTO ModifiedDate { get; set; }
        public StringDTO ModifiedDateToString { get; set; }
    }
    #endregion

    #region DepartmentModel
    public class _Department
    {
        public StringDTO Code { get; set; }
        public StringDTO Name { get; set; }
        public StringDTO CategoryName { get; set; }
        public StringDTO Active { get; set; }
    }
    #endregion

    #region StaffModel
    public class _Staff
    {
        public StringDTO Code { get; set; }
        public StringDTO Name { get; set; }
        public StringDTO DepartmentName { get; set; }
        public StringDTO DepartmentGroupName { get; set; }
        public StringDTO PositionName { get; set; }
        public StringDTO JoinedDateToString { get; set; }
        public StringDTO Active { get; set; }
        public StringDTO CreatedBy { get; set; }
        public StringDTO CreatedDateToString { get; set; }
        public StringDTO ModifiedBy { get; set; }
        public StringDTO ModifiedDateToString { get; set; }
    }
    #endregion

    #region WorkOrderModel
    public class _WorkOrderModel
    {
        public StringDTO Name { get; set; }
        public StringDTO ProductCode { get; set; }
        public StringDTO TotalQty { get; set; }
        public StringDTO DueDate { get; set; }
        public StringDTO BOMMasterCode { get; set; }

        public StringDTO MaterialCode { get; set; }
        public StringDTO ProcessCategoryName { get; set; }
        public StringDTO ProductionLineName { get; set; }
        public StringDTO ProcessLevel { get; set; }
        public StringDTO Target { get; set; }
        public StringDTO ScheduleTimeStart { get; set; }
        public StringDTO ScheduleTimeEnd { get; set; }
        public StringDTO Actual { get; set; }
        public StringDTO OKQty { get; set; }
        public StringDTO NGQty { get; set; }
        public StringDTO TimeStart { get; set; }
        public StringDTO TimeEnd { get; set; }
    }
    #endregion

    /// <summary>
    /// MULTIPLE LANGUAGES HELPER
    /// </summary>
    public class MultiLanguagesHelper
    {
        #region User
        public _UserDTO MultiLanguagesHelper_UserDTO()
        {
            return new _UserDTO
            {
                UserName = new StringDTO
                {
                    label = Resource.DbTbl_UserName
                },
                Name = new StringDTO
                {
                    label = Resource.DbTbl_Name
                },
                Address = new StringDTO
                {
                    label = Resource.DbTbl_Address
                },
                Email = new StringDTO
                {
                    label = Resource.DbTbl_Email
                },
                Phone = new StringDTO
                {
                    label = Resource.DbTbl_Phone
                }
            };
        }
        #endregion

        #region CommonMaster
        public _CommonMaster MultiLanguagesHelper_CommonMaster()
        {
            return new _CommonMaster
            {
                Code = new StringDTO
                {
                    label = Resource.DbTbl_Code
                },
                Name = new StringDTO
                {
                    label = Resource.DbTbl_Name
                }
            };
        }
        #endregion

        #region CommonDetail
        public _CommonDetail MultiLanguagesHelper_CommonDetail()
        {
            return new _CommonDetail
            {
                Code = new StringDTO
                {
                    label = Resource.DbTbl_Code
                },
                Name = new StringDTO
                {
                    label = Resource.DbTbl_Name
                },
                MasterName = new StringDTO
                {
                    label = Resource.DbTbl_MasterName
                },
                Active = new StringDTO
                {
                    label = Resource.DbTbl_Active
                }
            };
        }
        #endregion

        #region Permission
        public _Permission MultiLanguagesHelper_Permission()
        {
            return new _Permission
            {
                Name = new StringDTO
                {
                    label = Resource.DbTbl_Name
                },
                CategoryName = new StringDTO
                {
                    label = Resource.DbTbl_CategoryName
                },
                Description = new StringDTO
                {
                    label = Resource.DbTbl_Description
                },
                Active = new StringDTO
                {
                    label = Resource.DbTbl_Active
                }
            };
        }
        #endregion

        #region Menu
        public _Menu MultiLanguagesHelper_Menu()
        {
            return new _Menu
            {
                Code = new StringDTO
                {
                    label = Resource.DbTbl_Code
                },
                Name = new StringDTO
                {
                    label = Resource.DbTbl_Name
                },
                FullName = new StringDTO
                {
                    label = Resource.DbTbl_MenuFullName
                },
                CategoryName = new StringDTO
                {
                    label = Resource.DbTbl_CategoryName
                },
                Link = new StringDTO
                {
                    label = Resource.DbTbl_Link
                },
                Active = new StringDTO
                {
                    label = Resource.DbTbl_Active
                }
            };
        }
        #endregion

        #region Role
        public _Role MultiLanguagesHelper_Role()
        {
            return new _Role
            {
                Name = new StringDTO
                {
                    label = Resource.DbTbl_Name
                },
                CategoryName = new StringDTO
                {
                    label = Resource.DbTbl_CategoryName
                },
                Description = new StringDTO
                {
                    label = Resource.DbTbl_Description
                },
                Active = new StringDTO
                {
                    label = Resource.DbTbl_Active
                }
            };
        }
        #endregion

        #region Line Production
        public _Line MultiLanguagesHelper_Line()
        {
            return new _Line
            {
                Name = new StringDTO
                {
                    label = Resource.DbTbl_Code
                },

                Active = new StringDTO
                {
                    label = Resource.DbTbl_Active
                },

                ProcessCategory = new StringDTO
                {
                    label = Resource.DbTbl_ProcessCategory
                },
                Status = new StringDTO
                {
                    label = Resource.DbTbl_Status,
                }
            };
        }
        #endregion

        #region Product
        public _Product MultiLanguagesHelper_Product()
        {
            return new _Product
            {
                Code = new StringDTO
                {
                    label = Resource.DbTbl_Code
                },
                Description = new StringDTO
                {
                    label = Resource.DbTbl_Description
                },
                CategoryName = new StringDTO
                {
                    label = Resource.DbTbl_CategoryName
                },
                TypeName = new StringDTO
                {
                    label = Resource.DbTbl_TypeName
                },
                Active = new StringDTO
                {
                    label = Resource.DbTbl_Active
                },
                ApplyDateToString = new StringDTO
                {
                    label = Resource.DbTbl_ApplyDate
                },
                CreatedDateToString = new StringDTO
                {
                    label = Resource.DbTbl_CreatedDate
                },
                CreatedBy = new StringDTO
                {
                    label = Resource.DbTbl_CreateBy
                },
                ModifiedDateToString = new StringDTO
                {
                    label = Resource.DbTbl_ModifiedDate
                },
                ModifiedBy = new StringDTO
                {
                    label = Resource.DbTbl_ModifiedBy
                },
            };
        }
        #endregion

        #region Department
        public _Department MultiLanguagesHelper_Department()
        {
            return new _Department
            {
                Code = new StringDTO
                {
                    label = Resource.DbTbl_Code
                },
                Name = new StringDTO
                {
                    label = Resource.DbTbl_Name
                },
                CategoryName = new StringDTO
                {
                    label = Resource.DbTbl_CategoryName
                },

                Active = new StringDTO
                {
                    label = Resource.DbTbl_Active
                }
            };
        }
        #endregion

        #region Staff
        public _Staff MultiLanguagesHelper_Staff()
        {
            return new _Staff
            {
                Code = new StringDTO
                {
                    label = Resource.DbTbl_Code
                },
                Name = new StringDTO
                {
                    label = Resource.DbTbl_Name
                },
                DepartmentName = new StringDTO
                {
                    label = Resource.DbTbl_DepartmentName
                },
                DepartmentGroupName = new StringDTO
                {
                    label = Resource.DbTbl_DepartmentGroup
                },
                PositionName = new StringDTO
                {
                    label = Resource.DbTbl_PositionName
                },
                //JoinedDateToString = new StringDTO
                //{
                //    label = Resource.DbTbl_JoinedDateToString
                //},
                Active = new StringDTO
                {
                    label = Resource.DbTbl_Active
                },
                CreatedDateToString = new StringDTO
                {
                    label = Resource.DbTbl_CreatedDate
                },
                CreatedBy = new StringDTO
                {
                    label = Resource.DbTbl_CreateBy
                },
                ModifiedDateToString = new StringDTO
                {
                    label = Resource.DbTbl_ModifiedDate
                },
                ModifiedBy = new StringDTO
                {
                    label = Resource.DbTbl_ModifiedBy
                },
            };
        }
        #endregion

        #region UserGuide
        public NameUserGuide MultiLanguagesHelper_UserGuide()
        {
            return new NameUserGuide
            {
                Name = new StringDTO
                {
                    label = Resource.DbTbl_Name
                },
                Content = new StringDTO
                {
                    label = Resource.DbTbl_Content
                },

                MenuCode = new StringDTO
                {
                    label = Resource.DbTbl_Code
                },
                MenuLevel = new StringDTO
                {
                    label = Resource.DbTbl_MenuLevel
                },
                Language = new StringDTO
                {
                    label = Resource.DbTbl_Language
                },
                FullName = new StringDTO
                {
                    label = Resource.DbTbl_MenuFullName
                }


            };
        }
        #endregion

        #region MMSPlan
        public _WorkOrderModel MultiLanguagesHelper_WorkOrder()
        {
            return new _WorkOrderModel
            {
                //BOMMasterCode = new StringDTO
                //{
                //    label = Resource.DbTbl_BOMMasterCod
                //},
                DueDate = new StringDTO
                {
                    label = Resource.DbTbl_DueDate
                },
                MaterialCode = new StringDTO
                {
                    label = Resource.DbTbl_MaterialCode
                },
                Name = new StringDTO
                {
                    label = Resource.DbTbl_Name
                },
                ProcessCategoryName = new StringDTO
                {
                    label = Resource.DbTbl_Process
                },
                ProductionLineName = new StringDTO
                {
                    label = Resource.DbTbl_Line
                },
                ProcessLevel = new StringDTO
                {
                    label = Resource.DbTbl_ProcessLevel
                },
                ProductCode = new StringDTO
                {
                    label = Resource.DbTbl_ProductCode
                },
                ScheduleTimeEnd = new StringDTO
                {
                    label = Resource.DbTbl_ScheduleTimeEnd
                },
                ScheduleTimeStart = new StringDTO
                {
                    label = Resource.DbTbl_ScheduleTimeStart
                },
                Target = new StringDTO
                {
                    label = Resource.DbTbl_Target
                },
                TotalQty = new StringDTO
                {
                    label = Resource.DbTbl_TotalQuantity
                },
                Actual = new StringDTO
                {
                    label = Resource.DbTbl_Actual
                },
                OKQty = new StringDTO
                {
                    label = Resource.DbTbl_OKQty
                },
                NGQty = new StringDTO
                {
                    label = Resource.DbTbl_NGQty
                },
                TimeStart = new StringDTO
                {
                    label = Resource.DbTbl_TimeStart
                },
                TimeEnd = new StringDTO
                {
                    label = Resource.DbTbl_TimeEnd
                },
            };
        }
        #endregion

    }
}
