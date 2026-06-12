using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Models;
using WmsPlus.Api.Models.Entities;

namespace WmsPlus.Api.Data
{
    public class WarehouseDbContext : DbContext
    {
        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options)
        {
        }

        public DbSet<MfRktz> MfRktzs { get; set; }
        public DbSet<TfRktz> TfRktzs { get; set; }
        public DbSet<MyWh> MyWhs { get; set; }
        public DbSet<RkTypeSet> RkTypeSets { get; set; }

        // 储位上架单
        public DbSet<MfCwsj> MfCwsjs { get; set; }
        public DbSet<TfCwsj> TfCwsjs { get; set; }
        // 储位调拨单
        public DbSet<MfCwdb> MfCwdbs { get; set; }
        public DbSet<TfCwdb> TfCwdbs { get; set; }
        // 储位下架单
        public DbSet<MfCwxj> MfCwxjs { get; set; }
        public DbSet<TfCwxj> TfCwxjs { get; set; }
        // 调拨通知单
        public DbSet<MfIctz> MfIctzs { get; set; }
        public DbSet<TfIctz> TfIctzs { get; set; }
        // 库存调拨单
        public DbSet<MfIc> MfIcs { get; set; }
        // 库存调拨单表身
        public DbSet<TfIc> TfIcs { get; set; }
        // 直接拣货任务单
        public DbSet<MfXjrw> MfXjrws { get; set; }
        public DbSet<TfXjrw> TfXjrws { get; set; }

        // 盘点单据（单据确认作业）
        public DbSet<MfPd> MfPds { get; set; }
        public DbSet<TfPd> TfPds { get; set; }
        // 盘盈(验收入库)单（单据确认作业）
        public DbSet<MfYn> MfYns { get; set; }
        public DbSet<TfYn> TfYns { get; set; }
        // 盘亏(库存调整)单（单据确认作业）
    public DbSet<MfKu> MfKus { get; set; }
    public DbSet<TfKu> TfKus { get; set; }

    // 请检任务单
    public DbSet<MfQjrw> MfQjrws { get; set; }
    public DbSet<TfQjrw> TfQjrws { get; set; }

    // 检验单
    public DbSet<MfTy> MfTys { get; set; }
    public DbSet<TfTy> TfTys { get; set; }

    // 打印网点设置
    public DbSet<PrintSet> PrintSets { get; set; }
    // 打印服务任务
    public DbSet<PrintSerTask> PrintSerTasks { get; set; }

    // 货品属性信息设定
    public DbSet<PrdtPdaRn> PrdtPdaRns { get; set; }

    // 出库包装单
    public DbSet<MfPackage> MfPackages { get; set; }

    // 物流容器
    public DbSet<MfContain> MfContains { get; set; }
    // 物流容器类型设定
    public DbSet<ContainSet> ContainSets { get; set; }
    // 物流容器变动历史
        public DbSet<MfContainHis> MfContainHis { get; set; }

        // 波次单
        public DbSet<MfBc> MfBcs { get; set; }
        public DbSet<TfBc> TfBcs { get; set; }
        // 拣货单
        public DbSet<MfPk> MfPks { get; set; }
        public DbSet<TfPk> TfPks { get; set; }
        // 拣货退回单
        public DbSet<MfJt> MfJts { get; set; }
        // 出库退回通知单
        public DbSet<MfCktb> MfCktbs { get; set; }
        // 出库通知单
        public DbSet<MfCktz> MfCktzs { get; set; }
        public DbSet<TfCktz> TfCktzs { get; set; }
        // 出库单
        public DbSet<MfCk> MfCks { get; set; }
        // 波次拣货任务单
        public DbSet<MfJhrw> MfJhrws { get; set; }
        // 二次分拣单
        public DbSet<MfPkfj> MfPkfjs { get; set; }
        // 拣货报表
        public DbSet<TfRept> TfRepts { get; set; }

        // 盘盈(验收入库)单
        public DbSet<MfYb> MfYbs { get; set; }
        public DbSet<TfYb> TfYbs { get; set; }
        // 货品条码
        public DbSet<PrdtBarcode> PrdtBarcodes { get; set; }
        // 箱条码表
        public DbSet<PrdtBarcodeBox> PrdtBarcodeBoxes { get; set; }
        // 箱条码变动历史表
        public DbSet<BarBoxChange> BarBoxChanges { get; set; }
        // 序列号记录表
        public DbSet<BarRec> BarRecs { get; set; }
        // 条码编码规则表
        public DbSet<BarcodeRule> BarcodeRules { get; set; }
        // 条码属性表
        public DbSet<PswdProp> PswdProps { get; set; }
        // 拆码规则表头
        public DbSet<MfRemoveRule> MfRemoveRules { get; set; }
        // 拆码规则表身
        public DbSet<TfRemoveRule> TfRemoveRules { get; set; }
        // 供应商拆码规则表
        public DbSet<CusRemoveRule> CusRemoveRules { get; set; }
        // 货品条码打印套版表
        public DbSet<PrdtBarRpt> PrdtBarRpts { get; set; }
        // 出库通知单变动
        public DbSet<TfCktzChg> TfCktzChgs { get; set; }
        // 出库通知变更单表头
        public DbSet<MfCktzChg> MfCktzChgs { get; set; }

        // 拣货退回单表身
        public DbSet<TfJt> TfJts { get; set; }
        // 出库退回通知单表身
        public DbSet<TfCktb> TfCktbs { get; set; }
        // 二次分拣单表身
        public DbSet<TfPkfj> TfPkfjs { get; set; }
        // 波次拣货任务单表身
        public DbSet<TfJhrw> TfJhrws { get; set; }
        // 出库单表身
        public DbSet<TfCk> TfCks { get; set; }
        // 出库通知单附属信息
        public DbSet<TfCktzRcv> TfCktzRcvs { get; set; }
        // 出库单单据附属信息
        public DbSet<TfCkRcv> TfCkRcvs { get; set; }
        // 单据条码明细
        public DbSet<PdaBarCollect> PdaBarCollects { get; set; }
        // 仓库库存结余表
        public DbSet<Sprd> Sprds { get; set; }
        // 储位库存结余表
        public DbSet<SprdCw> SprdCws { get; set; }
        // 仓库库存表
        public DbSet<Prdt1> Prdt1s { get; set; }
        // 储位库存表
        public DbSet<Prdt1Cw> Prdt1Cws { get; set; }
        // 货品库存锁定表
        public DbSet<Prdt1Lock> Prdt1Locks { get; set; }
        // 部门表
        public DbSet<Dept> Depts { get; set; }
        // 拣货策略规则表
        public DbSet<PkRule> PkRules { get; set; }

        // 即时消息通知设定
        public DbSet<NoticeSet> NoticeSets { get; set; }

        // 货品特征码段设定
        public DbSet<PrdMark> PrdMarks { get; set; }

        // 货品主档
        public DbSet<Prdt> Prdts { get; set; }
        // 储存性质
        public DbSet<CwXz> CwXzs { get; set; }

        // 储位明细
        public DbSet<CwWh> CwWhs { get; set; }

        // 中类代号设定
        public DbSet<Indx> Indxes { get; set; }

        // 依储存性设定货品储位
        public DbSet<PrdtCwXz> PrdtCwXzs { get; set; }

        // 依仓库启用到货确认
        public DbSet<IzwhConfirm> IzwhConfirms { get; set; }

        // 依储类设定货品储位
        public DbSet<PrdtCw> PrdtCws { get; set; }

        // 下架策略
        public DbSet<XjRule> XjRules { get; set; }
        // 下架策略属性
        public DbSet<XjRuleProp> XjRuleProps { get; set; }

        // 波次策略
        public DbSet<BcRule> BcRules { get; set; }
        public DbSet<BcRuleProp> BcRuleProps { get; set; }

        // 不能参与配位的批号设置
        public DbSet<BatNotPw> BatNotPws { get; set; }

        // 单据类别设定
        public DbSet<BilSpc> BilSpcs { get; set; }

        // 行业代号设定
        public DbSet<DefNs> DefNss { get; set; }
        // 叉车车号管理
        public DbSet<ForkTruck> ForkTrucks { get; set; }

        // 查盘/与原因设定
        public DbSet<IjReasonSet> IjReasonSets { get; set; }

        // 系统设定
        public DbSet<SpcComp> SpcComps { get; set; }
    public DbSet<DrpProp> DrpProps { get; set; }

        // 不合格原因设定
        public DbSet<SpcSet> SpcSets { get; set; }

        // 车间入库单
        public DbSet<MfCj> MfCjs { get; set; }
        public DbSet<TfCj> TfCjs { get; set; }

    // 超交管制设定
    public DbSet<ExceedCtrl> ExceedCtrls { get; set; }

        // 收货单
    public DbSet<MfSh> MfShs { get; set; }
    public DbSet<TfSh> TfShs { get; set; }

    // 入库单
    public DbSet<MfRk> MfRks { get; set; }
    public DbSet<TfRk> TfRks { get; set; }

        // ========== 三级菜单界面 - 新增表注册开始 ==========
        
        // 类别权限设定
        public DbSet<Role> Roles { get; set; }
        public DbSet<FxPswd> FxPswds { get; set; }
        
        // 类别成员设定
        public DbSet<PswdRole> PswdRoles { get; set; }
        
        // 单据属性设定
        public DbSet<BarPswdProp> BarPswdProps { get; set; }
        
        // 自定义报表
        public DbSet<MfRpt> MfRpts { get; set; }
        public DbSet<TfRpt> TfRpts { get; set; }
        public DbSet<MfQry> MfQrys { get; set; }
        public DbSet<TfQry> TfQrys { get; set; }
        
        // 仿真布局设备设定
        public DbSet<EmulateSet> EmulateSets { get; set; }
        
        // 可视化仓储仿真布局设定
        public DbSet<MyWhView> MyWhViews { get; set; }
        
        // 服务日志查询表
        public DbSet<SvcLog> SvcLogs { get; set; }
        
        // 服务异常查询表
        public DbSet<SvcYc> SvcYcs { get; set; }
        
        // API接口异常表
        public DbSet<LkActionI> LkActionIs { get; set; }
        public DbSet<LkActionO> LkActionOs { get; set; }
        public DbSet<ApiActionI> ApiActionIs { get; set; }
        public DbSet<ApiActionO> ApiActionOs { get; set; }
        
        // 系统设备管理
        public DbSet<HwSet> HwSets { get; set; }
        public DbSet<HwSetP> HwSetPs { get; set; }
        
        // 第三方调用明细/历史表
        public DbSet<ApiActionHisI> ApiActionHisIs { get; set; }
        public DbSet<ApiActionHisO> ApiActionHisOs { get; set; }

        // ========== 三级菜单界面 - 新增表注册结束 ==========

        // 关账作业
        public DbSet<ConClose> ConCloses { get; set; }

        // 批号有效期修改历史
        public DbSet<ValidddUpdHis> ValidddUpdHises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 入库通知单表头
            modelBuilder.Entity<MfRktz>(entity =>
            {
                entity.ToTable("MF_RKTZ");
                entity.HasKey(e => e.TZ_NO);
                entity.Property(e => e.TZ_NO).HasColumnName("TZ_NO").HasMaxLength(100);
                entity.Property(e => e.TZ_DD).HasColumnName("TZ_DD");
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.SAL_NO).HasColumnName("SAL_NO").HasMaxLength(30);
                entity.Property(e => e.BIL_TYPE).HasColumnName("BIL_TYPE").HasMaxLength(10);
                entity.Property(e => e.CUS_NO).HasColumnName("CUS_NO").HasMaxLength(30);
                entity.Property(e => e.CUS_NAME).HasColumnName("CUS_NAME").HasMaxLength(300);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.EST_DD).HasColumnName("EST_DD");
                entity.Property(e => e.CLS_ID).HasColumnName("CLS_ID").HasMaxLength(1);
                entity.Property(e => e.REM).HasColumnName("REM");
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
            });

            // 入库通知单表身
            modelBuilder.Entity<TfRktz>(entity =>
            {
                entity.ToTable("TF_RKTZ");
                entity.HasKey(e => new { e.TZ_NO, e.ITM });
                entity.Property(e => e.TZ_NO).HasColumnName("TZ_NO").HasMaxLength(100);
                entity.Property(e => e.ITM).HasColumnName("ITM");
                entity.Property(e => e.TZ_DD).HasColumnName("TZ_DD");
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.PRD_NAME).HasColumnName("PRD_NAME").HasMaxLength(320);
                entity.Property(e => e.PRD_MARK).HasColumnName("PRD_MARK").HasMaxLength(255);
                entity.Property(e => e.BAT_NO).HasColumnName("BAT_NO").HasMaxLength(40);
                entity.Property(e => e.VALID_DD).HasColumnName("VALID_DD");
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.UNIT).HasColumnName("UNIT").HasMaxLength(1);
                entity.Property(e => e.QTY).HasColumnName("QTY");
                entity.Property(e => e.REM).HasColumnName("REM");
                entity.Property(e => e.CUS_NAME).HasColumnName("CUS_NAME").HasMaxLength(300);

                // 关联关系：表身 -> 表头
                entity.HasOne<MfRktz>()
                      .WithMany()
                      .HasForeignKey(e => e.TZ_NO)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // 仓库主数据
            modelBuilder.Entity<MyWh>(entity =>
            {
                entity.ToTable("MY_WH");
                entity.HasKey(e => e.WH);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.NAME).HasColumnName("NAME").HasMaxLength(400);
                entity.Property(e => e.WH_TYPE).HasColumnName("WH_TYPE").HasMaxLength(1);
                entity.Property(e => e.ATTRIB).HasColumnName("ATTRIB").HasMaxLength(1);
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.INVALID).HasColumnName("INVALID").HasMaxLength(1);
                entity.Property(e => e.CW_FLAG).HasColumnName("CW_FLAG").HasMaxLength(1);
                entity.Property(e => e.STOP_DD).HasColumnName("STOP_DD");
            });

            // 入库业务类型设置
            modelBuilder.Entity<RkTypeSet>(entity =>
            {
                entity.ToTable("cr_type_set");
                entity.HasKey(e => new { e.CR_TYPE, e.TYPE_ID });
                entity.Property(e => e.CR_TYPE).HasColumnName("CR_TYPE").HasMaxLength(1);
                entity.Property(e => e.TYPE_ID).HasColumnName("TYPE_ID").HasMaxLength(10);
                entity.Property(e => e.NAME).HasColumnName("NAME").HasMaxLength(300);
            });

            // 超交管制设置
            modelBuilder.Entity<ExceedCtrl>(entity =>
            {
                entity.ToTable("EXCEED_CTRL");
                entity.HasKey(e => new { e.CR_TYPE, e.TYPE_ID, e.PARAMS_ID });
                entity.Property(e => e.CR_TYPE).HasColumnName("CR_TYPE").HasMaxLength(1);
                entity.Property(e => e.GROUP_DEP).HasColumnName("GROUP_DEP").HasMaxLength(30);
                entity.Property(e => e.PARAMS_ID).HasColumnName("PARAMS_ID").HasMaxLength(20);
                entity.Property(e => e.PARAMS_VALUE).HasColumnName("PARAMS_VALUE").HasMaxLength(50);
                entity.Property(e => e.TYPE_ID).HasColumnName("TYPE_ID").HasMaxLength(10);
            });

            // 储位上架单表头
            modelBuilder.Entity<MfCwsj>(entity =>
            {
                entity.ToTable("MF_CWSJ");
                entity.HasKey(e => e.SJ_NO);
                entity.Property(e => e.SJ_NO).HasColumnName("SJ_NO").HasMaxLength(100);
                entity.Property(e => e.SJ_DD).HasColumnName("SJ_DD");
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.SAL_NO).HasColumnName("SAL_NO").HasMaxLength(30);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.BIL_TYPE).HasColumnName("BIL_TYPE").HasMaxLength(10);
                entity.Property(e => e.BIL_ID).HasColumnName("BIL_ID").HasMaxLength(20);
                entity.Property(e => e.BIL_NO).HasColumnName("BIL_NO").HasMaxLength(100);
            });

            // 储位上架单表身
            modelBuilder.Entity<TfCwsj>(entity =>
            {
                entity.ToTable("TF_CWSJ");
                entity.HasKey(e => new { e.SJ_NO, e.ITM });
                entity.Property(e => e.SJ_NO).HasColumnName("SJ_NO").HasMaxLength(100);
                entity.Property(e => e.ITM).HasColumnName("ITM");
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.PRD_NAME).HasColumnName("PRD_NAME").HasMaxLength(320);
                entity.Property(e => e.QTY).HasColumnName("QTY");
                entity.HasOne<MfCwsj>().WithMany().HasForeignKey(e => e.SJ_NO).OnDelete(DeleteBehavior.Restrict);
            });

            // 储位调拨单表头
            modelBuilder.Entity<MfCwdb>(entity =>
            {
                entity.ToTable("MF_CWDB");
                entity.HasKey(e => e.DB_NO);
                entity.Property(e => e.DB_NO).HasColumnName("DB_NO").HasMaxLength(100);
                entity.Property(e => e.DB_DD).HasColumnName("DB_DD");
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.SAL_NO).HasColumnName("SAL_NO").HasMaxLength(30);
                entity.Property(e => e.BIL_TYPE).HasColumnName("BIL_TYPE").HasMaxLength(10);
            });

            // 储位调拨单表身
            modelBuilder.Entity<TfCwdb>(entity =>
            {
                entity.ToTable("TF_CWDB");
                entity.HasKey(e => new { e.DB_NO, e.ITM });
                entity.Property(e => e.DB_NO).HasColumnName("DB_NO").HasMaxLength(100);
                entity.Property(e => e.ITM).HasColumnName("ITM");
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.PRD_NAME).HasColumnName("PRD_NAME").HasMaxLength(320);
                entity.Property(e => e.QTY).HasColumnName("QTY");
                entity.HasOne<MfCwdb>().WithMany().HasForeignKey(e => e.DB_NO).OnDelete(DeleteBehavior.Restrict);
            });

            // 储位下架单表头
            modelBuilder.Entity<MfCwxj>(entity =>
            {
                entity.ToTable("MF_CWXJ");
                entity.HasKey(e => e.XJ_NO);
                entity.Property(e => e.XJ_NO).HasColumnName("XJ_NO").HasMaxLength(100);
                entity.Property(e => e.XJ_DD).HasColumnName("XJ_DD");
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.SAL_NO).HasColumnName("SAL_NO").HasMaxLength(30);
                entity.Property(e => e.BIL_TYPE).HasColumnName("BIL_TYPE").HasMaxLength(10);
            });

            // 储位下架单表身
            modelBuilder.Entity<TfCwxj>(entity =>
            {
                entity.ToTable("TF_CWXJ");
                entity.HasKey(e => new { e.XJ_NO, e.ITM });
                entity.Property(e => e.XJ_NO).HasColumnName("XJ_NO").HasMaxLength(100);
                entity.Property(e => e.ITM).HasColumnName("ITM");
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.PRD_NAME).HasColumnName("PRD_NAME").HasMaxLength(160);
                entity.Property(e => e.QTY).HasColumnName("QTY");
                entity.HasOne<MfCwxj>().WithMany().HasForeignKey(e => e.XJ_NO).OnDelete(DeleteBehavior.Restrict);
            });

            // 调拨通知单表头
            modelBuilder.Entity<MfIctz>(entity =>
            {
                entity.ToTable("MF_ICTZ");
                entity.HasKey(e => e.TZ_NO);
                entity.Property(e => e.TZ_NO).HasColumnName("TZ_NO").HasMaxLength(100);
                entity.Property(e => e.TZ_DD).HasColumnName("TZ_DD");
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.SAL_NO).HasColumnName("SAL_NO").HasMaxLength(30);
                entity.Property(e => e.WH1).HasColumnName("WH1").HasMaxLength(30);
                entity.Property(e => e.WH2).HasColumnName("WH2").HasMaxLength(30);
                entity.Property(e => e.EST_DD).HasColumnName("EST_DD");
                entity.Property(e => e.UP_DD).HasColumnName("UP_DD");
                entity.Property(e => e.AREA_SH).HasColumnName("AREA_SH").HasMaxLength(50);
                entity.Property(e => e.CLS_ID_BC).HasColumnName("CLS_ID_BC").HasMaxLength(1);
                entity.Property(e => e.CLS_ID_CK).HasColumnName("CLS_ID_CK").HasMaxLength(1);
                entity.Property(e => e.TYPE_ID).HasColumnName("TYPE_ID").HasMaxLength(10);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
            });

            // 调拨通知单表身
            modelBuilder.Entity<TfIctz>(entity =>
            {
                entity.ToTable("TF_ICTZ");
                entity.HasKey(e => new { e.TZ_NO, e.ITM });
                entity.Property(e => e.TZ_NO).HasColumnName("TZ_NO").HasMaxLength(100);
                entity.Property(e => e.ITM).HasColumnName("ITM");
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.PRD_NAME).HasColumnName("PRD_NAME").HasMaxLength(300);
                entity.Property(e => e.QTY).HasColumnName("QTY");
                entity.Property(e => e.WH1).HasColumnName("WH1").HasMaxLength(30);
                entity.Property(e => e.WH2).HasColumnName("WH2").HasMaxLength(30);
                entity.HasOne<MfIctz>().WithMany().HasForeignKey(e => e.TZ_NO).OnDelete(DeleteBehavior.Restrict);
            });

            // 库存调拨单表头
            modelBuilder.Entity<MfIc>(entity =>
            {
                entity.ToTable("MF_IC");
                entity.HasKey(e => e.IC_NO);
                entity.Property(e => e.IC_NO).HasColumnName("IC_NO").HasMaxLength(100);
                entity.Property(e => e.IC_DD).HasColumnName("IC_DD");
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.SAL_NO).HasColumnName("SAL_NO").HasMaxLength(30);
                entity.Property(e => e.BIL_TYPE).HasColumnName("BIL_TYPE").HasMaxLength(10);
                entity.Property(e => e.BIL_NO).HasColumnName("BIL_NO").HasMaxLength(100);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
                entity.Property(e => e.MODIFY_DD).HasColumnName("MODIFY_DD");
                entity.Property(e => e.MODIFY_MAN).HasColumnName("MODIFY_MAN").HasMaxLength(30);
                entity.Property(e => e.PRT_DATE).HasColumnName("PRT_DATE");
                entity.Property(e => e.PRT_USR).HasColumnName("PRT_USR").HasMaxLength(30);
                entity.Property(e => e.PRT_SW).HasColumnName("PRT_SW").HasMaxLength(1);
                entity.Property(e => e.PICK_POINT).HasColumnName("PICK_POINT").HasMaxLength(200);
                entity.Property(e => e.WORK_STATION).HasColumnName("WORK_STATION").HasMaxLength(200);
                entity.Property(e => e.RECEI_AREA).HasColumnName("RECEI_AREA").HasMaxLength(50);
                entity.Property(e => e.AREA_SH).HasColumnName("AREA_SH").HasMaxLength(50);
                entity.Property(e => e.LINE_CODE).HasColumnName("LINE_CODE").HasMaxLength(50);
                entity.Property(e => e.SPAN_ERP_IC).HasColumnName("SPAN_ERP_IC").HasMaxLength(1);
                entity.Property(e => e.ERP_GEN_METHOD).HasColumnName("ERP_GEN_METHOD").HasMaxLength(20);
                entity.Property(e => e.TZ_NO_UO).HasColumnName("TZ_NO_UO").HasMaxLength(100);
            });

            // 库存调拨单表身
            modelBuilder.Entity<TfIc>(entity =>
            {
                entity.ToTable("TF_IC");
                entity.HasKey(e => new { e.IC_NO, e.ITM });
                entity.Property(e => e.IC_NO).HasColumnName("IC_NO").HasMaxLength(100);
                entity.Property(e => e.ITM).HasColumnName("ITM");
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.PRD_NAME).HasColumnName("PRD_NAME").HasMaxLength(300);
                entity.Property(e => e.QTY).HasColumnName("QTY");
                entity.HasOne<MfIc>().WithMany().HasForeignKey(e => e.IC_NO).OnDelete(DeleteBehavior.Restrict);
            });

            // 直接拣货任务单表头
            modelBuilder.Entity<MfXjrw>(entity =>
            {
                entity.ToTable("MF_XJRW");
                entity.HasKey(e => e.JR_NO);
                entity.Property(e => e.JR_NO).HasColumnName("JR_NO").HasMaxLength(100);
                entity.Property(e => e.JR_DD).HasColumnName("JR_DD");
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.SAL_NO).HasColumnName("SAL_NO").HasMaxLength(30);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.CLS_ID).HasColumnName("CLS_ID").HasMaxLength(1);
                entity.Property(e => e.TYPE_ID).HasColumnName("TYPE_ID").HasMaxLength(10);
                entity.Property(e => e.AREA_SH).HasColumnName("AREA_SH").HasMaxLength(50);
                entity.Property(e => e.PRIORITY_WCS).HasColumnName("PRIORITY_WCS");
                entity.Property(e => e.REM).HasColumnName("REM");
                entity.Property(e => e.CONTAIN_CODE).HasColumnName("CONTAIN_CODE").HasMaxLength(100);
                entity.Property(e => e.PICK_POINT).HasColumnName("PICK_POINT").HasMaxLength(200);
                entity.Property(e => e.WORK_STATION).HasColumnName("WORK_STATION").HasMaxLength(200);
                entity.Property(e => e.RECEI_AREA).HasColumnName("RECEI_AREA").HasMaxLength(50);
                entity.Property(e => e.SEND_ACTION).HasColumnName("SEND_ACTION").HasMaxLength(1);
                entity.Property(e => e.TYPE_CK).HasColumnName("TYPE_CK").HasMaxLength(1);
                entity.Property(e => e.CON_NO).HasColumnName("CON_NO").HasMaxLength(30);
                entity.Property(e => e.ACT_NO_OUT).HasColumnName("ACT_NO_OUT").HasMaxLength(20);
                entity.Property(e => e.PR_NO).HasColumnName("PR_NO").HasMaxLength(100);
                entity.Property(e => e.MODIFY_DD).HasColumnName("MODIFY_DD");
                entity.Property(e => e.PRT_DATE).HasColumnName("PRT_DATE");
                entity.Property(e => e.PRT_SW).HasColumnName("PRT_SW").HasMaxLength(1);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
            });

            // 直接拣货任务单表身
            modelBuilder.Entity<TfXjrw>(entity =>
            {
                entity.ToTable("TF_XJRW");
                entity.HasKey(e => new { e.JR_NO, e.ITM });
                entity.Property(e => e.JR_NO).HasColumnName("JR_NO").HasMaxLength(100);
                entity.Property(e => e.ITM).HasColumnName("ITM");
                entity.Property(e => e.TZ_ID).HasColumnName("TZ_ID");
                entity.Property(e => e.TZ_NO).HasColumnName("TZ_NO").HasMaxLength(100);
                entity.Property(e => e.TZ_ITM).HasColumnName("TZ_ITM");
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.PRD_NAME).HasColumnName("PRD_NAME").HasMaxLength(320);
                entity.Property(e => e.PRD_MARK).HasColumnName("PRD_MARK").HasMaxLength(255);
                entity.Property(e => e.BAT_NO).HasColumnName("BAT_NO").HasMaxLength(40);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.CHUW).HasColumnName("CHUW").HasMaxLength(30);
                entity.Property(e => e.UNIT).HasColumnName("UNIT").HasMaxLength(10);
                entity.Property(e => e.QTY).HasColumnName("QTY");
                entity.Property(e => e.QTY_PK).HasColumnName("QTY_PK");
                entity.Property(e => e.CONTAIN_CODE).HasColumnName("CONTAIN_CODE").HasMaxLength(100);
                entity.Property(e => e.CAR_NO).HasColumnName("CAR_NO").HasMaxLength(30);
                entity.Property(e => e.XJ_FLAG).HasColumnName("XJ_FLAG").HasMaxLength(1);
                entity.Property(e => e.ORG_BIL_NO).HasColumnName("ORG_BIL_NO").HasMaxLength(100);
                entity.Property(e => e.ORG_BIL_ITM).HasColumnName("ORG_BIL_ITM");
                entity.Property(e => e.ERP_BIL_ID).HasColumnName("ERP_BIL_ID");
                entity.Property(e => e.ERP_BIL_NO).HasColumnName("ERP_BIL_NO").HasMaxLength(100);
                entity.Property(e => e.ERP_BIL_ITM).HasColumnName("ERP_BIL_ITM");
                entity.Property(e => e.KEY_ITM).HasColumnName("KEY_ITM");

                // 关联关系：表身 -> 表头
                entity.HasOne<MfXjrw>()
                      .WithMany()
                      .HasForeignKey(e => e.JR_NO)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // 盘点单据表头（MF_PD）
            modelBuilder.Entity<MfPd>(entity =>
            {
                entity.ToTable("MF_PD");
                entity.HasKey(e => e.PD_NO);
                entity.Property(e => e.PD_NO).HasColumnName("PD_NO").HasMaxLength(100);
                entity.Property(e => e.PD_DD).HasColumnName("PD_DD");
                entity.Property(e => e.PD_DD1).HasColumnName("PD_DD1");
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.USR_PD).HasColumnName("USR_PD").HasMaxLength(30);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
                entity.Property(e => e.CUS_NO).HasColumnName("CUS_NO").HasMaxLength(30);
                entity.Property(e => e.CFM_SW).HasColumnName("CFM_SW").HasMaxLength(1);
                entity.Property(e => e.CFM_USR).HasColumnName("CFM_USR").HasMaxLength(30);
                entity.Property(e => e.CFM_DATE).HasColumnName("CFM_DATE");
                entity.Property(e => e.REM).HasColumnName("REM");
            });

            // 盘点单据表身（TF_PD）
            modelBuilder.Entity<TfPd>(entity =>
            {
                entity.ToTable("TF_PD");
                entity.HasKey(e => new { e.PD_NO, e.ITM });
                entity.Property(e => e.PD_NO).HasColumnName("PD_NO").HasMaxLength(100);
                entity.Property(e => e.ITM).HasColumnName("ITM");
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.QTY1).HasColumnName("QTY1");
                entity.Property(e => e.QTY2).HasColumnName("QTY2");
                entity.Property(e => e.QTY_RNG).HasColumnName("QTY_RNG");
                entity.Property(e => e.REM).HasColumnName("REM");

                entity.HasOne<MfPd>().WithMany().HasForeignKey(e => e.PD_NO).OnDelete(DeleteBehavior.Restrict);
            });

            // 盘盈(验收入库)单表头（MF_YN）
            modelBuilder.Entity<MfYn>(entity =>
            {
                entity.ToTable("MF_YN");
                entity.HasKey(e => e.YN_NO);
                entity.Property(e => e.YN_NO).HasColumnName("YN_NO").HasMaxLength(100);
                entity.Property(e => e.YN_DD).HasColumnName("YN_DD");
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
                entity.Property(e => e.CFM_SW).HasColumnName("CFM_SW").HasMaxLength(1);
                entity.Property(e => e.CFM_USR).HasColumnName("CFM_USR").HasMaxLength(30);
                entity.Property(e => e.CFM_DATE).HasColumnName("CFM_DATE");
                entity.Property(e => e.REM).HasColumnName("REM");
            });

            // 盘盈(验收入库)单表身（TF_YN）
            modelBuilder.Entity<TfYn>(entity =>
            {
                entity.ToTable("TF_YN");
                entity.HasKey(e => new { e.YN_NO, e.ITM });
                entity.Property(e => e.YN_NO).HasColumnName("YN_NO").HasMaxLength(100);
                entity.Property(e => e.ITM).HasColumnName("ITM");
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.PRD_NAME).HasColumnName("PRD_NAME").HasMaxLength(320);
                entity.Property(e => e.QTY).HasColumnName("QTY");
                entity.Property(e => e.REM).HasColumnName("REM");

                entity.HasOne<MfYn>().WithMany().HasForeignKey(e => e.YN_NO).OnDelete(DeleteBehavior.Restrict);
            });

            // 盘亏(库存调整)单表头（MF_KU）
            modelBuilder.Entity<MfKu>(entity =>
            {
                entity.ToTable("MF_KU");
                entity.HasKey(e => e.KU_NO);
                entity.Property(e => e.KU_NO).HasColumnName("KU_NO").HasMaxLength(100);
                entity.Property(e => e.KU_DD).HasColumnName("KU_DD");
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
                entity.Property(e => e.CFM_SW).HasColumnName("CFM_SW").HasMaxLength(1);
                entity.Property(e => e.CFM_USR).HasColumnName("CFM_USR").HasMaxLength(30);
                entity.Property(e => e.CFM_DATE).HasColumnName("CFM_DATE");
                entity.Property(e => e.REM).HasColumnName("REM");
            });

            // 盘亏(库存调整)单表身（TF_KU）
            modelBuilder.Entity<TfKu>(entity =>
            {
                entity.ToTable("TF_KU");
                entity.HasKey(e => new { e.KU_NO, e.ITM });
                entity.Property(e => e.KU_NO).HasColumnName("KU_NO").HasMaxLength(100);
                entity.Property(e => e.ITM).HasColumnName("ITM");
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.PRD_NAME).HasColumnName("PRD_NAME").HasMaxLength(320);
                entity.Property(e => e.QTY).HasColumnName("QTY");
                entity.Property(e => e.REM).HasColumnName("REM");

                entity.HasOne<MfKu>().WithMany().HasForeignKey(e => e.KU_NO).OnDelete(DeleteBehavior.Restrict);
            });

            // 请检任务单表头（MF_QJRW）
            modelBuilder.Entity<MfQjrw>(entity =>
            {
                entity.ToTable("MF_QJRW");
                entity.HasKey(e => e.QJ_NO);
                entity.Property(e => e.QJ_NO).HasColumnName("QJ_NO").HasMaxLength(100);
                entity.Property(e => e.QJ_DD).HasColumnName("QJ_DD");
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.WH_TY).HasColumnName("WH_TY").HasMaxLength(30);
                entity.Property(e => e.SAL_NO).HasColumnName("SAL_NO").HasMaxLength(30);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.REM).HasColumnName("REM");
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
                entity.Property(e => e.CON_NO).HasColumnName("CON_NO").HasMaxLength(30);
                entity.Property(e => e.BIL_KND).HasColumnName("BIL_KND").HasMaxLength(10);
                entity.Property(e => e.TN_NO).HasColumnName("TN_NO").HasMaxLength(100);
                entity.Property(e => e.XJ_FLAG).HasColumnName("XJ_FLAG").HasMaxLength(1);
            });

            // 请检任务单表身（TF_QJRW）
            modelBuilder.Entity<TfQjrw>(entity =>
            {
                entity.ToTable("TF_QJRW");
                entity.HasKey(e => new { e.QJ_NO, e.ITM });
                entity.Property(e => e.QJ_NO).HasColumnName("QJ_NO").HasMaxLength(100);
                entity.Property(e => e.ITM).HasColumnName("ITM");
                entity.Property(e => e.CON_NO).HasColumnName("CON_NO").HasMaxLength(30);
                entity.Property(e => e.TN_NO).HasColumnName("TN_NO").HasMaxLength(100);

                entity.HasOne<MfQjrw>().WithMany().HasForeignKey(e => e.QJ_NO).OnDelete(DeleteBehavior.Restrict);
            });

            // 检验单表头（MF_TY）
            modelBuilder.Entity<MfTy>(entity =>
            {
                entity.ToTable("MF_TY");
                entity.HasKey(e => e.TY_NO);
                entity.Property(e => e.TY_NO).HasColumnName("TY_NO").HasMaxLength(100);
                entity.Property(e => e.TY_DD).HasColumnName("TY_DD");
                entity.Property(e => e.BIL_KND).HasColumnName("BIL_KND").HasMaxLength(10);
                entity.Property(e => e.TYWZ).HasColumnName("TYWZ").HasMaxLength(30);
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.BIL_TYPE).HasColumnName("BIL_TYPE").HasMaxLength(10);
                entity.Property(e => e.TYPE_ID).HasColumnName("TYPE_ID").HasMaxLength(10);
                entity.Property(e => e.BIL_NO).HasColumnName("BIL_NO").HasMaxLength(100);
                entity.Property(e => e.REM).HasColumnName("REM");
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.CUS_NO).HasColumnName("CUS_NO").HasMaxLength(30);
                entity.Property(e => e.CUS_NAME).HasColumnName("CUS_NAME").HasMaxLength(300);
                entity.Property(e => e.CON_NO).HasColumnName("CON_NO").HasMaxLength(30);
                entity.Property(e => e.OTH_BIL_NO).HasColumnName("OTH_BIL_NO").HasMaxLength(100);
                entity.Property(e => e.CLS_ID_SPC).HasColumnName("CLS_ID_SPC").HasMaxLength(1);
                entity.Property(e => e.TZ_NO_UO).HasColumnName("TZ_NO_UO").HasMaxLength(100);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
            });

            // 检验单表身（TF_TY）
            modelBuilder.Entity<TfTy>(entity =>
            {
                entity.ToTable("TF_TY");
                entity.HasKey(e => new { e.TY_NO, e.ITM });
                entity.Property(e => e.TY_NO).HasColumnName("TY_NO").HasMaxLength(100);
                entity.Property(e => e.ITM).HasColumnName("ITM");

                // 关联关系：表身 -> 表头
                entity.HasOne<MfTy>()
                      .WithMany()
                      .HasForeignKey(e => e.TY_NO)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // 货品属性信息设定（PRDT_PDA_RN）
            modelBuilder.Entity<PrdtPdaRn>(entity =>
            {
                entity.ToTable("PRDT_PDA_RN");
                entity.HasKey(e => e.PRD_NO);
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.QTY_COLLECT).HasColumnName("QTY_COLLECT").HasMaxLength(20);
                entity.Property(e => e.BARCODE_TYPE).HasColumnName("BARCODE_TYPE").HasMaxLength(20);
                entity.Property(e => e.NEED_SCALE).HasColumnName("NEED_SCALE").HasMaxLength(10);
                entity.Property(e => e.QTY_QZ_MODE).HasColumnName("QTY_QZ_MODE").HasMaxLength(20);
                entity.Property(e => e.UT_POINT).HasColumnName("UT_POINT");
                entity.Property(e => e.UT1_POINT).HasColumnName("UT1_POINT");
                entity.Property(e => e.UT1_DISP).HasColumnName("UT1_DISP").HasMaxLength(50);
                entity.Property(e => e.QTY_TYPE).HasColumnName("QTY_TYPE").HasMaxLength(20);
                entity.Property(e => e.SHOW_TYPE).HasColumnName("SHOW_TYPE").HasMaxLength(20);
                entity.Property(e => e.SCALE_POINT).HasColumnName("SCALE_POINT");
                entity.Property(e => e.SCALE_QZ).HasColumnName("SCALE_QZ").HasMaxLength(20);
                entity.Property(e => e.SHOW_PAK).HasColumnName("SHOW_PAK").HasMaxLength(10);
            });

            // 打印网点设置（PRINT_SET）
            modelBuilder.Entity<PrintSet>(entity =>
            {
                entity.ToTable("PRINT_SET");
                entity.HasKey(e => e.SEQ_NO);
                entity.Property(e => e.SEQ_NO).HasColumnName("SEQ_NO");
                entity.Property(e => e.SITE_NAME).HasColumnName("SITE_NAME").HasMaxLength(200);
                entity.Property(e => e.MACHINE_IP).HasColumnName("MACHINE_IP").HasMaxLength(50);
                entity.Property(e => e.MACHINE_NAME).HasColumnName("MACHINE_NAME").HasMaxLength(200);
                entity.Property(e => e.STOP_FLAG).HasColumnName("STOP_FLAG").HasMaxLength(1);
                entity.Property(e => e.STOP_DATE).HasColumnName("STOP_DATE");
            });

            // 打印服务任务（PRINT_SER_TASK）
            modelBuilder.Entity<PrintSerTask>(entity =>
            {
                entity.ToTable("PRINT_SER_TASK");
                entity.HasKey(e => e.SEQ_NO);
                entity.Property(e => e.SEQ_NO).HasColumnName("SEQ_NO");
                entity.Property(e => e.PRINT_TIME).HasColumnName("PRINT_TIME");
                entity.Property(e => e.VERSION_CODE).HasColumnName("VERSION_CODE").HasMaxLength(50);
                entity.Property(e => e.PRINTER_USER).HasColumnName("PRINTER_USER").HasMaxLength(50);
                entity.Property(e => e.SITE_NAME).HasColumnName("SITE_NAME").HasMaxLength(200);
                entity.Property(e => e.PROGRAM_CODE).HasColumnName("PROGRAM_CODE").HasMaxLength(50);
                entity.Property(e => e.TEMPLATE_CODE).HasColumnName("TEMPLATE_CODE").HasMaxLength(50);
                entity.Property(e => e.PRINT_STATUS).HasColumnName("PRINT_STATUS").HasMaxLength(20);
                entity.Property(e => e.FAIL_COUNT).HasColumnName("FAIL_COUNT");
                entity.Property(e => e.FAIL_REASON).HasColumnName("FAIL_REASON").HasMaxLength(500);
                entity.Property(e => e.PRINT_NO).HasColumnName("PRINT_NO").HasMaxLength(100);
            });

            // 出库包装单表头（MF_PACKAGE）
            modelBuilder.Entity<MfPackage>(entity =>
            {
                entity.ToTable("MF_PACKAGE");
                entity.HasKey(e => e.PACKAGE_NO);
                entity.Property(e => e.PACKAGE_NO).HasColumnName("PACKAGE_NO").HasMaxLength(100);
                entity.Property(e => e.PACKAGE_DD).HasColumnName("PACKAGE_DD");
                entity.Property(e => e.TYPE_ID).HasColumnName("TYPE_ID").HasMaxLength(10);
                entity.Property(e => e.PACKAGER).HasColumnName("PACKAGER").HasMaxLength(30);
                entity.Property(e => e.PACK_TIME).HasColumnName("PACK_TIME");
                entity.Property(e => e.OUT_BIL_NO).HasColumnName("OUT_BIL_NO").HasMaxLength(100);
                entity.Property(e => e.CUS_NO).HasColumnName("CUS_NO").HasMaxLength(30);
                entity.Property(e => e.OUT_STATUS).HasColumnName("OUT_STATUS").HasMaxLength(10);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.BIL_TYPE).HasColumnName("BIL_TYPE").HasMaxLength(10);
                entity.Property(e => e.REM).HasColumnName("REM");
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
            });

            // 物流容器（MF_CONTAIN）
            modelBuilder.Entity<MfContain>(entity =>
            {
                entity.ToTable("MF_CONTAIN");
                entity.HasKey(e => e.CONTAIN_CODE);
                entity.Property(e => e.CONTAIN_CODE).HasColumnName("CONTAIN_CODE").HasMaxLength(100);
                entity.Property(e => e.SCAN_CODE).HasColumnName("SCAN_CODE").HasMaxLength(200);
                entity.Property(e => e.CONTAIN_TYPE).HasColumnName("CONTAIN_TYPE").HasMaxLength(20);
                entity.Property(e => e.CONTAIN_STATUS).HasColumnName("CONTAIN_STATUS").HasMaxLength(10);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.CHUW).HasColumnName("CHUW").HasMaxLength(30);
                entity.Property(e => e.CHUW_POS).HasColumnName("CHUW_POS").HasMaxLength(200);
                entity.Property(e => e.TRANSIT_FLAG).HasColumnName("TRANSIT_FLAG").HasMaxLength(1);
                entity.Property(e => e.INSPECT_FLAG).HasColumnName("INSPECT_FLAG").HasMaxLength(1);
                entity.Property(e => e.CONTAIN_DETAIL).HasColumnName("CONTAIN_DETAIL").HasMaxLength(500);
                entity.Property(e => e.MODIFY_HISTORY).HasColumnName("MODIFY_HISTORY").HasMaxLength(500);
                entity.Property(e => e.COMBINE_NO).HasColumnName("COMBINE_NO").HasMaxLength(50);
                entity.Property(e => e.PRT_DATE).HasColumnName("PRT_DATE");
                entity.Property(e => e.INVENTORY_DATE).HasColumnName("INVENTORY_DATE");
                entity.Property(e => e.INVENTORY_QTY).HasColumnName("INVENTORY_QTY").HasMaxLength(30);
            });

            // 物流容器类型设定（CONTAIN_SET）
            modelBuilder.Entity<ContainSet>(entity =>
            {
                entity.ToTable("CONTAIN_SET");
                entity.HasKey(e => e.TYPE_CODE);
                entity.Property(e => e.TYPE_CODE).HasColumnName("TYPE_CODE").HasMaxLength(20);
                entity.Property(e => e.TYPE_NAME).HasColumnName("TYPE_NAME").HasMaxLength(100);
                entity.Property(e => e.CODE_PREFIX).HasColumnName("CODE_PREFIX").HasMaxLength(20);
                entity.Property(e => e.STOP_FLAG).HasColumnName("STOP_FLAG").HasMaxLength(1);
                entity.Property(e => e.IS_SYSTEM).HasColumnName("IS_SYSTEM").HasMaxLength(1);
                entity.Property(e => e.RCS_TYPE).HasColumnName("RCS_TYPE").HasMaxLength(20);
            });

            // 物流容器变动历史（MF_CONTAIN_HIS）
            modelBuilder.Entity<MfContainHis>(entity =>
            {
                entity.ToTable("MF_CONTAIN_HIS");
                entity.HasKey(e => e.CONTAIN_CODE);  // 实际可能有复合主键，此处简化
                entity.Property(e => e.CONTAIN_CODE).HasColumnName("CONTAIN_CODE").HasMaxLength(100);
                entity.Property(e => e.CONTAIN_STATUS).HasColumnName("CONTAIN_STATUS").HasMaxLength(10);
                entity.Property(e => e.CONTAIN_TYPE).HasColumnName("CONTAIN_TYPE").HasMaxLength(20);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.TRANSIT_FLAG).HasColumnName("TRANSIT_FLAG").HasMaxLength(1);
                entity.Property(e => e.INSPECT_FLAG).HasColumnName("INSPECT_FLAG").HasMaxLength(1);
                entity.Property(e => e.CHANGE_DOC_NAME).HasColumnName("CHANGE_DOC_NAME").HasMaxLength(100);
                entity.Property(e => e.CHANGE_NO).HasColumnName("CHANGE_NO").HasMaxLength(100);
                entity.Property(e => e.CHANGE_MAN).HasColumnName("CHANGE_MAN").HasMaxLength(30);
                entity.Property(e => e.CHANGE_TIME).HasColumnName("CHANGE_TIME");
            });

            // 波次单表头（MF_BC）
            modelBuilder.Entity<MfBc>(entity =>
            {
                entity.ToTable("MF_BC");
                entity.HasKey(e => e.BC_NO);
                entity.Property(e => e.BC_NO).HasColumnName("BC_NO").HasMaxLength(100);
                entity.Property(e => e.BC_DD).HasColumnName("BC_DD");
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.SAL_NO).HasColumnName("SAL_NO").HasMaxLength(30);
                entity.Property(e => e.BIL_TYPE).HasColumnName("BIL_TYPE").HasMaxLength(10);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.CLS_ID).HasColumnName("CLS_ID").HasMaxLength(1);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
            });

            // 波次单表身（TF_BC）
            modelBuilder.Entity<TfBc>(entity =>
            {
                entity.ToTable("TF_BC");
                entity.HasKey(e => new { e.BC_NO, e.ITM });
                entity.Property(e => e.BC_NO).HasColumnName("BC_NO").HasMaxLength(100);
                entity.Property(e => e.ITM).HasColumnName("ITM");
                entity.Property(e => e.BC_DD).HasColumnName("BC_DD");
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.PRD_NAME).HasColumnName("PRD_NAME").HasMaxLength(320);
                entity.Property(e => e.BAT_NO).HasColumnName("BAT_NO").HasMaxLength(40);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.UNIT).HasColumnName("UNIT").HasMaxLength(10);
                entity.Property(e => e.QTY).HasColumnName("QTY");
                entity.Property(e => e.PICK_QTY).HasColumnName("PICK_QTY");
                entity.Property(e => e.REM).HasColumnName("REM");
                entity.Property(e => e.PRD_MARK).HasColumnName("PRD_MARK").HasMaxLength(255);

                entity.HasOne<MfBc>().WithMany().HasForeignKey(e => e.BC_NO).OnDelete(DeleteBehavior.Restrict);
            });

            // 拣货单表头（MF_PK）
            modelBuilder.Entity<MfPk>(entity =>
            {
                entity.ToTable("MF_PK");
                entity.HasKey(e => e.PK_NO);
                entity.Property(e => e.PK_NO).HasColumnName("PK_NO").HasMaxLength(100);
                entity.Property(e => e.PK_DD).HasColumnName("PK_DD");
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.SAL_NO).HasColumnName("SAL_NO").HasMaxLength(30);
                entity.Property(e => e.BIL_TYPE).HasColumnName("BIL_TYPE").HasMaxLength(10);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.CLS_ID).HasColumnName("CLS_ID").HasMaxLength(1);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
            });

            // 拣货单表身（TF_PK）
            modelBuilder.Entity<TfPk>(entity =>
            {
                entity.ToTable("TF_PK");
                entity.HasKey(e => new { e.PK_NO, e.ITM });
                entity.Property(e => e.PK_NO).HasColumnName("PK_NO").HasMaxLength(100);
                entity.Property(e => e.ITM).HasColumnName("ITM");
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.PRD_NAME).HasColumnName("PRD_NAME").HasMaxLength(320);
                entity.Property(e => e.QTY).HasColumnName("QTY");
                entity.Property(e => e.QTY_PK).HasColumnName("QTY_PK");

                entity.HasOne<MfPk>().WithMany().HasForeignKey(e => e.PK_NO).OnDelete(DeleteBehavior.Restrict);
            });

            // 拣货退回单表头（MF_JT）
            modelBuilder.Entity<MfJt>(entity =>
            {
                entity.ToTable("MF_JT");
                entity.HasKey(e => e.JT_NO);
                entity.Property(e => e.JT_NO).HasColumnName("JT_NO").HasMaxLength(100);
                entity.Property(e => e.JT_DD).HasColumnName("JT_DD");
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.SAL_NO).HasColumnName("SAL_NO").HasMaxLength(30);
                entity.Property(e => e.CLS_ID).HasColumnName("CLS_ID").HasMaxLength(1);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
            });

            // 出库退回通知单表头（MF_CKTB）
            modelBuilder.Entity<MfCktb>(entity =>
            {
                entity.ToTable("MF_CKTB");
                entity.HasKey(e => e.TB_NO);
                entity.Property(e => e.TB_NO).HasColumnName("TB_NO").HasMaxLength(100);
                entity.Property(e => e.TB_DD).HasColumnName("TB_DD");
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.SAL_NO).HasColumnName("SAL_NO").HasMaxLength(30);
                entity.Property(e => e.BIL_TYPE).HasColumnName("BIL_TYPE").HasMaxLength(10);
                entity.Property(e => e.CUS_NO).HasColumnName("CUS_NO").HasMaxLength(30);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.CLS_ID).HasColumnName("CLS_ID").HasMaxLength(1);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
            });

            // 出库通知单表头（MF_CKTZ）
            modelBuilder.Entity<MfCktz>(entity =>
            {
                entity.ToTable("MF_CKTZ");
                entity.HasKey(e => e.TZ_NO);
                entity.Property(e => e.TZ_NO).HasColumnName("TZ_NO").HasMaxLength(100);
                entity.Property(e => e.TZ_DD).HasColumnName("TZ_DD");
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.SAL_NO).HasColumnName("SAL_NO").HasMaxLength(30);
                entity.Property(e => e.BIL_TYPE).HasColumnName("BIL_TYPE").HasMaxLength(10);
                entity.Property(e => e.CUS_NO).HasColumnName("CUS_NO").HasMaxLength(30);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.CLS_ID).HasColumnName("CLS_ID").HasMaxLength(1);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
            });

            // 出库通知单表身（TF_CKTZ）
            modelBuilder.Entity<TfCktz>(entity =>
            {
                entity.ToTable("TF_CKTZ");
                entity.HasKey(e => new { e.TZ_NO, e.ITM });
                entity.Property(e => e.TZ_NO).HasColumnName("TZ_NO").HasMaxLength(100);
                entity.Property(e => e.ITM).HasColumnName("ITM");
                entity.Property(e => e.TZ_DD).HasColumnName("TZ_DD");
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.PRD_NAME).HasColumnName("PRD_NAME").HasMaxLength(320);
                entity.Property(e => e.PRD_MARK).HasColumnName("PRD_MARK").HasMaxLength(255);
                entity.Property(e => e.BAT_NO).HasColumnName("BAT_NO").HasMaxLength(40);
                entity.Property(e => e.VALID_DD).HasColumnName("VALID_DD");
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.UNIT).HasColumnName("UNIT").HasMaxLength(10);
                entity.Property(e => e.QTY).HasColumnName("QTY");
                entity.Property(e => e.REM).HasColumnName("REM");
                entity.Property(e => e.CUS_NAME).HasColumnName("CUS_NAME").HasMaxLength(300);
                entity.Property(e => e.BC_NO).HasColumnName("BC_NO").HasMaxLength(100);
                entity.Property(e => e.CONVERT_QTY).HasColumnName("CONVERT_QTY");
                entity.Property(e => e.RETURN_QTY).HasColumnName("RETURN_QTY");
                entity.Property(e => e.PICK_QTY).HasColumnName("PICK_QTY");

                entity.HasOne<MfCktz>().WithMany().HasForeignKey(e => e.TZ_NO).OnDelete(DeleteBehavior.Restrict);
            });

            // 出库单表头（MF_CK）
            modelBuilder.Entity<MfCk>(entity =>
            {
                entity.ToTable("MF_CK");
                entity.HasKey(e => e.CK_ID);
                entity.Property(e => e.CK_ID).HasColumnName("CK_ID").HasMaxLength(100);
                entity.Property(e => e.CK_DD).HasColumnName("CK_DD");
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.SAL_NO).HasColumnName("SAL_NO").HasMaxLength(30);
                entity.Property(e => e.BIL_TYPE).HasColumnName("BIL_TYPE").HasMaxLength(10);
                entity.Property(e => e.CUS_NO).HasColumnName("CUS_NO").HasMaxLength(30);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.CLS_ID).HasColumnName("CLS_ID").HasMaxLength(1);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
            });

            // 波次拣货任务单表头（MF_JHRW）
            modelBuilder.Entity<MfJhrw>(entity =>
            {
                entity.ToTable("MF_JHRW");
                entity.HasKey(e => e.JR_NO);
                entity.Property(e => e.JR_NO).HasColumnName("JR_NO").HasMaxLength(100);
                entity.Property(e => e.JR_DD).HasColumnName("JR_DD");
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.SAL_NO).HasColumnName("SAL_NO").HasMaxLength(30);
                entity.Property(e => e.CLS_ID).HasColumnName("CLS_ID").HasMaxLength(1);
                entity.Property(e => e.TYPE_ID).HasColumnName("TYPE_ID").HasMaxLength(10);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
            });

            // 二次分拣单表头（MF_PKFJ）
            modelBuilder.Entity<MfPkfj>(entity =>
            {
                entity.ToTable("MF_PKFJ");
                entity.HasKey(e => e.PKFJ_NO);
                entity.Property(e => e.PKFJ_NO).HasColumnName("PKFJ_NO").HasMaxLength(100);
                entity.Property(e => e.PKFJ_DD).HasColumnName("PKFJ_DD");
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.CLS_ID).HasColumnName("CLS_ID").HasMaxLength(1);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
            });

            // 货品主档（PRDT）
            modelBuilder.Entity<Prdt>(entity =>
            {
                entity.ToTable("PRDT");
                entity.HasKey(e => e.PRD_NO);
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.NAME).HasColumnName("NAME").HasMaxLength(320);
                entity.Property(e => e.SNM).HasColumnName("SNM").HasMaxLength(100);
                entity.Property(e => e.IDX1).HasColumnName("IDX1").HasMaxLength(30);
                entity.Property(e => e.UT).HasColumnName("UT").HasMaxLength(10);
                entity.Property(e => e.SPC).HasColumnName("SPC").HasMaxLength(100);
                entity.Property(e => e.CWXZ_NO).HasColumnName("CWXZ_NO").HasMaxLength(20);
                entity.Property(e => e.NOUSE_DD).HasColumnName("NOUSE_DD");
            });

            // 拣货报表（TF_REPT）
            modelBuilder.Entity<TfRept>(entity =>
            {
                entity.ToTable("TF_REPT");
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).HasColumnName("ID");
                entity.Property(e => e.REPT_NO).HasColumnName("REPT_NO").HasMaxLength(100);
                entity.Property(e => e.ITM).HasColumnName("ITM");
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.PRD_NAME).HasColumnName("PRD_NAME").HasMaxLength(320);
                entity.Property(e => e.QTY).HasColumnName("QTY");
                entity.Property(e => e.QTY_PK).HasColumnName("QTY_PK");
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.CHUW).HasColumnName("CHUW").HasMaxLength(30);
            });

            // 货品特征码段设定（PRD_MARK）
            modelBuilder.Entity<PrdMark>(entity =>
            {
                entity.ToTable("PRD_MARK");
                entity.HasKey(e => e.MOB_ID);
                entity.Property(e => e.MOB_ID).HasColumnName("MOB_ID").HasMaxLength(50);
                entity.Property(e => e.MOB_NAME).HasColumnName("MOB_NAME").HasMaxLength(200);
                entity.Property(e => e.PRD_MARK).HasColumnName("PRD_MARK").HasMaxLength(255);
                entity.Property(e => e.REM).HasColumnName("REM").HasMaxLength(500);
                entity.Property(e => e.END_DD).HasColumnName("END_DD");
            });

            // 储存性质（CW_XZ）
            modelBuilder.Entity<CwXz>(entity =>
            {
                entity.ToTable("CW_XZ");
                entity.HasKey(e => e.CWXZ_NO);
                entity.Property(e => e.CWXZ_NO).HasColumnName("CWXZ_NO").HasMaxLength(30);
                entity.Property(e => e.NAME).HasColumnName("NAME").HasMaxLength(400);
                entity.Property(e => e.UP_DD).HasColumnName("UP_DD");
            });

            // 储位明细（CW_WH）
            modelBuilder.Entity<CwWh>(entity =>
            {
                entity.ToTable("CW_WH");
                entity.HasKey(e => e.CHUW);
                entity.Property(e => e.CHUW).HasColumnName("CHUW").HasMaxLength(30);
                entity.Property(e => e.NAME).HasColumnName("NAME").HasMaxLength(400);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.GS).HasColumnName("GS").HasMaxLength(30);
                entity.Property(e => e.GL).HasColumnName("GL").HasMaxLength(30);
                entity.Property(e => e.LAYER).HasColumnName("LAYER").HasMaxLength(30);
                entity.Property(e => e.CW_STATUS).HasColumnName("CW_STATUS").HasMaxLength(10);
                entity.Property(e => e.LOCK_CW).HasColumnName("LOCK_CW").HasMaxLength(1);
                entity.Property(e => e.AREA_ID).HasColumnName("AREA_ID").HasMaxLength(30);
            });

            // 中类代号设定（INDX）
            modelBuilder.Entity<Indx>(entity =>
            {
                entity.ToTable("INDX");
                entity.HasKey(e => e.IDX_NO);
                entity.Property(e => e.IDX_NO).HasColumnName("IDX_NO").HasMaxLength(30);
                entity.Property(e => e.NAME).HasColumnName("NAME").HasMaxLength(400);
                entity.Property(e => e.IDX_UP).HasColumnName("IDX_UP").HasMaxLength(30);
                entity.Property(e => e.STOP_DD).HasColumnName("STOP_DD");
                entity.Property(e => e.REM).HasColumnName("REM");
            });

            // 依储存性设定货品储位（PRDT_CW_XZ）
            modelBuilder.Entity<PrdtCwXz>(entity =>
            {
                entity.ToTable("PRDT_CW_XZ");
                entity.HasKey(e => e.GUID);
                entity.Property(e => e.GUID).HasColumnName("GUID").HasMaxLength(50);
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.CWXZ_NO).HasColumnName("CWXZ_NO").HasMaxLength(30);
                entity.Property(e => e.CHUW).HasColumnName("CHUW").HasMaxLength(30);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.GS).HasColumnName("GS").HasMaxLength(10);
                entity.Property(e => e.GL).HasColumnName("GL").HasMaxLength(10);
                entity.Property(e => e.LAYER).HasColumnName("LAYER").HasMaxLength(10);
                entity.Property(e => e.ZONE_ID).HasColumnName("ZONE_ID").HasMaxLength(30);
                entity.Property(e => e.CON_NO).HasColumnName("CON_NO").HasMaxLength(30);
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.REM).HasColumnName("REM").HasMaxLength(500);
                entity.Property(e => e.UP_DD).HasColumnName("UP_DD");
            });

            // 依仓库启用到货确认（IZWH_CONFIRM）
            modelBuilder.Entity<IzwhConfirm>(entity =>
            {
                entity.ToTable("IZWH_CONFIRM");
                entity.HasKey(e => new { e.WH_OUT, e.WH_IN });
                entity.Property(e => e.WH_OUT).HasColumnName("WH_OUT").HasMaxLength(30);
                entity.Property(e => e.WH_IN).HasColumnName("WH_IN").HasMaxLength(30);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.MODIFY_MAN).HasColumnName("MODIFY_MAN").HasMaxLength(30);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
                entity.Property(e => e.MODIFY_DD).HasColumnName("MODIFY_DD");
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
            });

            // 依储类设定货品储位（PRDT_CW）
            modelBuilder.Entity<PrdtCw>(entity =>
            {
                entity.ToTable("PRDT_CW");
                entity.HasKey(e => e.GUID);
                entity.Property(e => e.GUID).HasColumnName("GUID").HasMaxLength(50);
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.PRD_MARK).HasColumnName("PRD_MARK").HasMaxLength(255);
                entity.Property(e => e.IDX_NO).HasColumnName("IDX_NO").HasMaxLength(30);
                entity.Property(e => e.CHUW).HasColumnName("CHUW").HasMaxLength(30);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.GS).HasColumnName("GS").HasMaxLength(20);
                entity.Property(e => e.GL).HasColumnName("GL").HasMaxLength(20);
                entity.Property(e => e.LAYER).HasColumnName("LAYER").HasMaxLength(20);
                entity.Property(e => e.ZONE_ID).HasColumnName("ZONE_ID").HasMaxLength(30);
                entity.Property(e => e.CON_NO).HasColumnName("CON_NO").HasMaxLength(30);
                entity.Property(e => e.REM).HasColumnName("REM").HasMaxLength(500);
                entity.Property(e => e.UP_DD).HasColumnName("UP_DD");
            });

            // 下架策略（XJ_RULE）
            modelBuilder.Entity<XjRule>(entity =>
            {
                entity.ToTable("XJ_RULE");
                entity.HasKey(e => e.RULE_ID);
                entity.Property(e => e.RULE_ID).HasColumnName("RULE_ID").HasMaxLength(50);
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.NAME).HasColumnName("NAME").HasMaxLength(200);
                entity.Property(e => e.WH_TYPE).HasColumnName("WH_TYPE").HasMaxLength(1);
                entity.Property(e => e.STOP_ID).HasColumnName("STOP_ID").HasMaxLength(1);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
                entity.Property(e => e.MODIFY_MAN).HasColumnName("MODIFY_MAN").HasMaxLength(30);
                entity.Property(e => e.MODIFY_DD).HasColumnName("MODIFY_DD");
            });

            // 下架策略属性（XJ_RULE_PROP）
            modelBuilder.Entity<XjRuleProp>(entity =>
            {
                entity.ToTable("XJ_RULE_PROP");
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).HasColumnName("ID");
                entity.Property(e => e.RULE_ID).HasColumnName("RULE_ID").HasMaxLength(50);
                entity.Property(e => e.PROP_TYPE).HasColumnName("PROP_TYPE").HasMaxLength(20);
                entity.Property(e => e.PROP_CODE).HasColumnName("PROP_CODE").HasMaxLength(50);
                entity.Property(e => e.PROP_NAME).HasColumnName("PROP_NAME").HasMaxLength(200);
                entity.Property(e => e.PROP_VALUE).HasColumnName("PROP_VALUE").HasMaxLength(500);
            });

            // 不能参与配位的批号设置（BAT_NOT_PW）
            modelBuilder.Entity<BatNotPw>(entity =>
            {
                entity.ToTable("BAT_NOT_PW");
                entity.HasKey(e => e.GUID);
                entity.Property(e => e.GUID).HasColumnName("GUID").HasMaxLength(50);
                entity.Property(e => e.CON_NO).HasColumnName("CON_NO").HasMaxLength(30);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.PRD_MARK).HasColumnName("PRD_MARK").HasMaxLength(255);
                entity.Property(e => e.BAT_NO).HasColumnName("BAT_NO").HasMaxLength(40);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
                entity.Property(e => e.REM).HasColumnName("REM").HasMaxLength(500);
            });

            // 拣货策略（PK_RULE）
            modelBuilder.Entity<PkRule>(entity =>
            {
                entity.ToTable("PK_RULE");
                entity.HasKey(e => e.RULE_ID);
                entity.Property(e => e.RULE_ID).HasColumnName("RULE_ID").HasMaxLength(50);
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.NAME).HasColumnName("NAME").HasMaxLength(200);
                entity.Property(e => e.WH_TYPE).HasColumnName("WH_TYPE").HasMaxLength(10);
                entity.Property(e => e.STOP_ID).HasColumnName("STOP_ID").HasMaxLength(1);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
                entity.Property(e => e.MODIFY_MAN).HasColumnName("MODIFY_MAN").HasMaxLength(30);
                entity.Property(e => e.MODIFY_DD).HasColumnName("MODIFY_DD");
            });

            // 波次策略（BC_RULE）
            modelBuilder.Entity<BcRule>(entity =>
            {
                entity.ToTable("BC_RULE");
                entity.HasKey(e => e.RULE_ID);
                entity.Property(e => e.RULE_ID).HasColumnName("RULE_ID").HasMaxLength(50);
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.NAME).HasColumnName("NAME").HasMaxLength(200);
                entity.Property(e => e.WH_TYPE).HasColumnName("WH_TYPE").HasMaxLength(1);
                entity.Property(e => e.STOP_ID).HasColumnName("STOP_ID").HasMaxLength(1);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
                entity.Property(e => e.MODIFY_MAN).HasColumnName("MODIFY_MAN").HasMaxLength(30);
                entity.Property(e => e.MODIFY_DD).HasColumnName("MODIFY_DD");
            });

            // 波次策略属性（BC_RULE_PROP）
            modelBuilder.Entity<BcRuleProp>(entity =>
            {
                entity.ToTable("BC_RULE_PROP");
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).HasColumnName("ID");
                entity.Property(e => e.RULE_ID).HasColumnName("RULE_ID").HasMaxLength(50);
                entity.Property(e => e.PROP_TYPE).HasColumnName("PROP_TYPE").HasMaxLength(20);
                entity.Property(e => e.PROP_CODE).HasColumnName("PROP_CODE").HasMaxLength(50);
                entity.Property(e => e.PROP_NAME).HasColumnName("PROP_NAME").HasMaxLength(200);
                entity.Property(e => e.PROP_VALUE).HasColumnName("PROP_VALUE").HasMaxLength(500);
            });

            // 货品条码表（PRDT_BARCODE）
            modelBuilder.Entity<PrdtBarcode>(entity =>
            {
                entity.ToTable("PRDT_BARCODE");
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).HasColumnName("ID");
                entity.Property(e => e.SCAN_CODE).HasColumnName("SCAN_CODE").HasMaxLength(200);
                entity.Property(e => e.BARCODE).HasColumnName("BARCODE").HasMaxLength(200);
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.PRD_NAME).HasColumnName("PRD_NAME").HasMaxLength(320);
                entity.Property(e => e.BAT_NO).HasColumnName("BAT_NO").HasMaxLength(40);
                entity.Property(e => e.SOURCE_NO).HasColumnName("SOURCE_NO").HasMaxLength(100);
                entity.Property(e => e.SOURCE_DOC).HasColumnName("SOURCE_DOC").HasMaxLength(20);
                entity.Property(e => e.VALID_DATE).HasColumnName("VALID_DATE");
                entity.Property(e => e.LAST_PRINT_TIME).HasColumnName("LAST_PRINT_TIME");
                entity.Property(e => e.QTY).HasColumnName("QTY");
                entity.Property(e => e.PRINTED_QTY).HasColumnName("PRINTED_QTY");
                entity.Property(e => e.LABEL_COUNT).HasColumnName("LABEL_COUNT");
                entity.Property(e => e.PRINT_BARCODE).HasColumnName("PRINT_BARCODE").HasMaxLength(200);
                entity.Property(e => e.CUS_NO).HasColumnName("CUS_NO").HasMaxLength(30);
                entity.Property(e => e.CUS_NAME).HasColumnName("CUS_NAME").HasMaxLength(300);
                entity.Property(e => e.SO_NO).HasColumnName("SO_NO").HasMaxLength(100);
                entity.Property(e => e.SEQ_NO).HasColumnName("SEQ_NO");
                entity.Property(e => e.ORIG_QTY).HasColumnName("ORIG_QTY");
                entity.Property(e => e.CREATE_DD).HasColumnName("CREATE_DD");
                entity.Property(e => e.INPUT_USR).HasColumnName("INPUT_USR").HasMaxLength(30);
                entity.Property(e => e.INPUT_BATCH).HasColumnName("INPUT_BATCH").HasMaxLength(50);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
            });

            // 箱条码表（PRDT_BARCODE_BOX）
            modelBuilder.Entity<PrdtBarcodeBox>(entity =>
            {
                entity.ToTable("PRDT_BARCODE_BOX");
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).HasColumnName("ID");
                entity.Property(e => e.SCAN_CODE).HasColumnName("SCAN_CODE").HasMaxLength(200);
                entity.Property(e => e.BOX_BARCODE).HasColumnName("BOX_BARCODE").HasMaxLength(200);
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.PRD_NAME).HasColumnName("PRD_NAME").HasMaxLength(320);
                entity.Property(e => e.BAT_NO).HasColumnName("BAT_NO").HasMaxLength(40);
                entity.Property(e => e.QTY).HasColumnName("QTY");
                entity.Property(e => e.SOURCE_NO).HasColumnName("SOURCE_NO").HasMaxLength(100);
                entity.Property(e => e.SOURCE_ITM).HasColumnName("SOURCE_ITM");
                entity.Property(e => e.VALID_DATE).HasColumnName("VALID_DATE");
                entity.Property(e => e.CHANGE_HISTORY).HasColumnName("CHANGE_HISTORY").HasMaxLength(500);
                entity.Property(e => e.LAST_PRINT_TIME).HasColumnName("LAST_PRINT_TIME");
                entity.Property(e => e.OUTER_BOX_FLAG).HasColumnName("OUTER_BOX_FLAG").HasMaxLength(1);
                entity.Property(e => e.SPECIAL_INSPECT).HasColumnName("SPECIAL_INSPECT").HasMaxLength(1);
                entity.Property(e => e.INVENTORY_DATE).HasColumnName("INVENTORY_DATE");
                entity.Property(e => e.SHOW_EMPTY_ONLY).HasColumnName("SHOW_EMPTY_ONLY");
            });

            // 箱条码变动历史表（BAR_BOX_CHANGE）
            modelBuilder.Entity<BarBoxChange>(entity =>
            {
                entity.ToTable("BAR_BOX_CHANGE");
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).HasColumnName("ID");
                entity.Property(e => e.SEQ_NO).HasColumnName("SEQ_NO");
                entity.Property(e => e.CHANGE_TIME).HasColumnName("CHANGE_TIME");
                entity.Property(e => e.BOX_BARCODE).HasColumnName("BOX_BARCODE").HasMaxLength(200);
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.PRD_NAME).HasColumnName("PRD_NAME").HasMaxLength(320);
                entity.Property(e => e.BAT_NO).HasColumnName("BAT_NO").HasMaxLength(40);
                entity.Property(e => e.SOURCE_DOC_TYPE).HasColumnName("SOURCE_DOC_TYPE").HasMaxLength(20);
                entity.Property(e => e.DOC_NAME).HasColumnName("DOC_NAME").HasMaxLength(100);
            });

            // 序列号记录表（BAR_REC）
            modelBuilder.Entity<BarRec>(entity =>
            {
                entity.ToTable("BAR_REC");
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).HasColumnName("ID");
                entity.Property(e => e.SCAN_CODE).HasColumnName("SCAN_CODE").HasMaxLength(200);
                entity.Property(e => e.SERIAL_NO).HasColumnName("SERIAL_NO").HasMaxLength(200);
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.PRD_NAME).HasColumnName("PRD_NAME").HasMaxLength(320);
                entity.Property(e => e.BAT_NO).HasColumnName("BAT_NO").HasMaxLength(40);
                entity.Property(e => e.SOURCE_NO).HasColumnName("SOURCE_NO").HasMaxLength(100);
                entity.Property(e => e.SOURCE_ITM).HasColumnName("SOURCE_ITM");
                entity.Property(e => e.VALID_DATE).HasColumnName("VALID_DATE");
                entity.Property(e => e.LAST_PRINT_TIME).HasColumnName("LAST_PRINT_TIME");
                entity.Property(e => e.SERIAL_FROM).HasColumnName("SERIAL_FROM").HasMaxLength(100);
                entity.Property(e => e.SERIAL_TO).HasColumnName("SERIAL_TO").HasMaxLength(100);
                entity.Property(e => e.INVENTORY_DATE).HasColumnName("INVENTORY_DATE");
                entity.Property(e => e.SHOW_EMPTY_ONLY).HasColumnName("SHOW_EMPTY_ONLY");
            });

            // 条码编码规则表（BARCODE_RULE）
            modelBuilder.Entity<BarcodeRule>(entity =>
            {
                entity.ToTable("BARCODE_RULE");
                entity.HasKey(e => e.RULE_ID);
                entity.Property(e => e.RULE_ID).HasColumnName("RULE_ID").HasMaxLength(50);
                entity.Property(e => e.RuleType).HasColumnName("RULE_TYPE").HasMaxLength(20);
                entity.Property(e => e.RuleCode).HasColumnName("RULE_CODE").HasMaxLength(50);
                entity.Property(e => e.RuleName).HasColumnName("RULE_NAME").HasMaxLength(200);
                entity.Property(e => e.FlowLength).HasColumnName("FLOW_LENGTH");
                entity.Property(e => e.Separator).HasColumnName("SEPARATOR").HasMaxLength(10);
                entity.Property(e => e.Prefix).HasColumnName("PREFIX").HasMaxLength(50);
            });

            // 条码属性表（PSWD_PROP）
            modelBuilder.Entity<PswdProp>(entity =>
            {
                entity.ToTable("PSWD_PROP");
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).HasColumnName("ID");
                entity.Property(e => e.PROP_TYPE).HasColumnName("PROP_TYPE").HasMaxLength(20);
                entity.Property(e => e.PROP_CODE).HasColumnName("PROP_CODE").HasMaxLength(50);
                entity.Property(e => e.PROP_NAME).HasColumnName("PROP_NAME").HasMaxLength(200);
                entity.Property(e => e.PROP_VALUE).HasColumnName("PROP_VALUE").HasMaxLength(500);
            });

            // 拆码规则表头（MF_REMVOE_RULE）
            modelBuilder.Entity<MfRemoveRule>(entity =>
            {
                entity.ToTable("MF_REMVOE_RULE");
                entity.HasKey(e => e.RULE_ID);
                entity.Property(e => e.RULE_ID).HasColumnName("RULE_ID").HasMaxLength(50);
                entity.Property(e => e.RULE_CODE).HasColumnName("RULE_CODE").HasMaxLength(50);
                entity.Property(e => e.RULE_NAME).HasColumnName("RULE_NAME").HasMaxLength(200);
                entity.Property(e => e.BASE_BARCODE).HasColumnName("BASE_BARCODE").HasMaxLength(200);
                entity.Property(e => e.ENCODING_METHOD).HasColumnName("ENCODING_METHOD").HasMaxLength(20);
                entity.Property(e => e.SEPARATOR).HasColumnName("SEPARATOR").HasMaxLength(10);
                entity.Property(e => e.TOTAL_LENGTH).HasColumnName("TOTAL_LENGTH");
                entity.Property(e => e.DEFAULT_FLAG).HasColumnName("DEFAULT_FLAG").HasMaxLength(1);
            });

            // 拆码规则表身（TF_REMVOE_RULE）
            modelBuilder.Entity<TfRemoveRule>(entity =>
            {
                entity.ToTable("TF_REMVOE_RULE");
                entity.HasKey(e => new { e.RULE_ID, e.ITM });
                entity.Property(e => e.RULE_ID).HasColumnName("RULE_ID").HasMaxLength(50);
                entity.Property(e => e.ITM).HasColumnName("ITM");
                entity.Property(e => e.FIELD_NAME).HasColumnName("FIELD_NAME").HasMaxLength(50);
                entity.Property(e => e.FIELD_POS).HasColumnName("FIELD_POS");
                entity.Property(e => e.FIELD_LEN).HasColumnName("FIELD_LEN");

                entity.HasOne<MfRemoveRule>().WithMany().HasForeignKey(e => e.RULE_ID).OnDelete(DeleteBehavior.Restrict);
            });

            // 供应商拆码规则表（CUS_REMVOE_RULE）
            modelBuilder.Entity<CusRemoveRule>(entity =>
            {
                entity.ToTable("CUS_REMVOE_RULE");
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).HasColumnName("ID");
                entity.Property(e => e.SEQ_NO).HasColumnName("SEQ_NO");
                entity.Property(e => e.CUS_NO).HasColumnName("CUS_NO").HasMaxLength(30);
                entity.Property(e => e.CUS_NAME).HasColumnName("CUS_NAME").HasMaxLength(300);
                entity.Property(e => e.RULE_CODE).HasColumnName("RULE_CODE").HasMaxLength(50);
                entity.Property(e => e.RULE_NAME).HasColumnName("RULE_NAME").HasMaxLength(200);
            });

            // 货品条码打印套版表（PRDT_BAR_RPT）
            modelBuilder.Entity<PrdtBarRpt>(entity =>
            {
                entity.ToTable("PRDT_BAR_RPT");
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).HasColumnName("ID");
                entity.Property(e => e.BAR_TYPE).HasColumnName("BAR_TYPE").HasMaxLength(20);
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.PRD_NAME).HasColumnName("PRD_NAME").HasMaxLength(320);
                entity.Property(e => e.MID_CLASS_NO).HasColumnName("MID_CLASS_NO").HasMaxLength(30);
                entity.Property(e => e.MID_CLASS_NAME).HasColumnName("MID_CLASS_NAME").HasMaxLength(200);
                entity.Property(e => e.TEMPLATE_CODE).HasColumnName("TEMPLATE_CODE").HasMaxLength(50);
                entity.Property(e => e.TEMPLATE_NAME).HasColumnName("TEMPLATE_NAME").HasMaxLength(200);
            });

            // 盘盈(验收入库)单表头（MF_YB）
            modelBuilder.Entity<MfYb>(entity =>
            {
                entity.ToTable("MF_YB");
                entity.HasKey(e => e.YB_NO);
                entity.Property(e => e.YB_NO).HasColumnName("YB_NO").HasMaxLength(100);
                entity.Property(e => e.YB_DD).HasColumnName("YB_DD");
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
                entity.Property(e => e.BIL_KND).HasColumnName("BIL_KND").HasMaxLength(10);
                entity.Property(e => e.BIL_TYPE).HasColumnName("BIL_TYPE").HasMaxLength(10);
                entity.Property(e => e.TY_NO).HasColumnName("TY_NO").HasMaxLength(100);
                entity.Property(e => e.REM).HasColumnName("REM");
                entity.Property(e => e.CUS_NO).HasColumnName("CUS_NO").HasMaxLength(30);
                entity.Property(e => e.CUS_NAME).HasColumnName("CUS_NAME").HasMaxLength(300);
                entity.Property(e => e.TYPE_ID).HasColumnName("TYPE_ID").HasMaxLength(10);
            });

            // 盘盈(验收入库)单表身（TF_YB）
            modelBuilder.Entity<TfYb>(entity =>
            {
                entity.ToTable("TF_YB");
                entity.HasKey(e => new { e.YB_NO, e.ITM });
                entity.Property(e => e.YB_NO).HasColumnName("YB_NO").HasMaxLength(100);
                entity.Property(e => e.ITM).HasColumnName("ITM");
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.PRD_NAME).HasColumnName("PRD_NAME").HasMaxLength(320);
                entity.Property(e => e.QTY).HasColumnName("QTY");
                entity.Property(e => e.REM).HasColumnName("REM");

                entity.Property(e => e.TY_NO).HasColumnName("TY_NO").HasMaxLength(100);
                entity.Property(e => e.TYPE_ID).HasColumnName("TYPE_ID").HasMaxLength(10);

                entity.HasOne<MfYb>().WithMany().HasForeignKey(e => e.YB_NO).OnDelete(DeleteBehavior.Restrict);
            });

            // 部门表（DEPT）
            modelBuilder.Entity<Dept>(entity =>
            {
                entity.ToTable("DEPT");
                entity.HasKey(e => e.DEP);
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.NAME).HasColumnName("NAME").HasMaxLength(400);
                entity.Property(e => e.ENG_NAME).HasColumnName("ENG_NAME").HasMaxLength(200);
                entity.Property(e => e.UP).HasColumnName("UP").HasMaxLength(30);
                entity.Property(e => e.STOP_DD).HasColumnName("STOP_DD");
                entity.Property(e => e.MAKE_ID).HasColumnName("MAKE_ID").HasMaxLength(10);
                entity.Property(e => e.GROUP_ID).HasColumnName("GROUP_ID").HasMaxLength(30);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
                entity.Property(e => e.UP_DD).HasColumnName("UP_DD");
                entity.Property(e => e.NAME_PY).HasColumnName("NAME_PY").HasMaxLength(200);
                entity.Property(e => e.TP_ID).HasColumnName("TP_ID").HasMaxLength(10);
            });

            // 即时消息通知设定表（NOTICE_SET）
            modelBuilder.Entity<NoticeSet>(entity =>
            {
                entity.ToTable("NOTICE_SET");
                entity.HasKey(e => e.SET_NO);
                entity.Property(e => e.SET_NO).HasColumnName("SET_NO").HasMaxLength(50);
                entity.Property(e => e.TYPE_ID).HasColumnName("TYPE_ID").HasMaxLength(20);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.SEND_OBJ).HasColumnName("SEND_OBJ").HasMaxLength(200);
                entity.Property(e => e.SEND_USRS).HasColumnName("SEND_USRS").HasMaxLength(500);
                entity.Property(e => e.SEND_TYPE).HasColumnName("SEND_TYPE").HasMaxLength(20);
                entity.Property(e => e.STOP_ID).HasColumnName("STOP_ID").HasMaxLength(1);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
            });

            // 出库通知变更单表头（MF_CKTZ_CHG）
            modelBuilder.Entity<MfCktzChg>(entity =>
            {
                entity.ToTable("MF_CKTZ_CHG");
                entity.HasKey(e => e.CHG_NO);
                entity.Property(e => e.CHG_NO).HasColumnName("CHG_NO").HasMaxLength(100);
                entity.Property(e => e.CHG_DATE).HasColumnName("CHG_DATE");
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.BIL_TYPE).HasColumnName("BIL_TYPE").HasMaxLength(10);
                entity.Property(e => e.TZ_NO).HasColumnName("TZ_NO").HasMaxLength(100);
                entity.Property(e => e.EXE_STATUS).HasColumnName("EXE_STATUS").HasMaxLength(10);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.USR_NAME).HasColumnName("USR_NAME").HasMaxLength(100);
                entity.Property(e => e.CHK_MAN).HasColumnName("CHK_MAN").HasMaxLength(30);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
                entity.Property(e => e.REM).HasColumnName("REM");
            });

            // 出库通知变更单表身（TF_CKTZ_CHG）
            modelBuilder.Entity<TfCktzChg>(entity =>
            {
                entity.ToTable("TF_CKTZ_CHG");
                entity.HasKey(e => new { e.CHG_NO, e.ITM });
                entity.Property(e => e.CHG_NO).HasColumnName("CHG_NO").HasMaxLength(100);
                entity.Property(e => e.ITM).HasColumnName("ITM");
                entity.Property(e => e.CHG_DD).HasColumnName("CHG_DD");
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.PRD_NAME).HasColumnName("PRD_NAME").HasMaxLength(320);
                entity.Property(e => e.PRD_MARK).HasColumnName("PRD_MARK").HasMaxLength(255);
                entity.Property(e => e.BAT_NO).HasColumnName("BAT_NO").HasMaxLength(40);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.UNIT).HasColumnName("UNIT").HasMaxLength(10);
                entity.Property(e => e.QTY).HasColumnName("QTY");
                entity.Property(e => e.REM).HasColumnName("REM");

                entity.HasOne<MfCktzChg>().WithMany().HasForeignKey(e => e.CHG_NO).OnDelete(DeleteBehavior.Restrict);
            });

            // 查盘/与原因设定表（IJ_REASON_SET）
            modelBuilder.Entity<IjReasonSet>(entity =>
            {
                entity.ToTable("IJ_REASON_SET");
                entity.HasKey(e => new { e.BIL_ID, e.IJ_REASON });
                entity.Property(e => e.BIL_ID).HasColumnName("BIL_ID").HasMaxLength(20);
                entity.Property(e => e.IJ_REASON).HasColumnName("IJ_REASON").HasMaxLength(50);
                entity.Property(e => e.REASON_REM).HasColumnName("REASON_REM").HasMaxLength(200);
            });

            // 单据类别设定表（BIL_SPC）
            modelBuilder.Entity<BilSpc>(entity =>
            {
                entity.ToTable("BIL_SPC");
                entity.HasKey(e => new { e.SPC_ID, e.SPC_NO });
                entity.Property(e => e.SPC_ID).HasColumnName("SPC_ID").HasMaxLength(10);
                entity.Property(e => e.SPC_NO).HasColumnName("SPC_NO").HasMaxLength(10);
                entity.Property(e => e.NAME).HasColumnName("NAME").HasMaxLength(300);
                entity.Property(e => e.REM).HasColumnName("REM");
            });

            // 行业代号设定表（DEF_NS）
            modelBuilder.Entity<DefNs>(entity =>
            {
                entity.ToTable("DEF_NS");
                entity.HasKey(e => e.NS_NO);
                entity.Property(e => e.NS_NO).HasColumnName("NS_NO").HasMaxLength(30);
                entity.Property(e => e.NAME).HasColumnName("NAME").HasMaxLength(400);
                entity.Property(e => e.REM).HasColumnName("REM");
                entity.Property(e => e.INC_SYS).HasColumnName("INC_SYS").HasMaxLength(1);
                entity.Property(e => e.INC_UNI).HasColumnName("INC_UNI").HasMaxLength(1);
            });

            // 叉车车号管理表（FORK_TRUCK）
            modelBuilder.Entity<ForkTruck>(entity =>
            {
                entity.ToTable("FORK_TRUCK");
                entity.HasKey(e => e.TRUCK_NO);
                entity.Property(e => e.TRUCK_NO).HasColumnName("TRUCK_NO").HasMaxLength(30);
                entity.Property(e => e.NAME).HasColumnName("NAME").HasMaxLength(200);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.REM).HasColumnName("REM");
            });

            // 系统参数/公司设定表（SPC_COMP）
            modelBuilder.Entity<SpcComp>(entity =>
            {
                entity.ToTable("SPC_COMP");
                entity.HasKey(e => new { e.DEP, e.CTRL_ID });
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(4);
                entity.Property(e => e.CTRL_ID).HasColumnName("CTRL_ID").HasMaxLength(50);
                entity.Property(e => e.SPC_ID).HasColumnName("SPC_ID").HasMaxLength(200);
                entity.Property(e => e.REM).HasColumnName("REM").HasMaxLength(200);
                entity.Property(e => e.UP_DD).HasColumnName("UP_DD");
            });

            // 属性/下拉选项参数表（DRP_PROP）
            modelBuilder.Entity<DrpProp>(entity =>
            {
                entity.ToTable("DRP_PROP");
                entity.HasKey(e => new { e.DEP, e.ITEM });
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(4);
                entity.Property(e => e.ITEM).HasColumnName("ITEM");
                entity.Property(e => e.VALUE).HasColumnName("VALUE").HasMaxLength(200);
                entity.Property(e => e.REM).HasColumnName("REM").HasMaxLength(200);
                entity.Property(e => e.UP_DD).HasColumnName("UP_DD");
            });

            // 收货单表头（MF_SH）
            modelBuilder.Entity<MfSh>(entity =>
            {
                entity.ToTable("MF_SH");
                entity.HasKey(e => e.SH_NO);
                entity.Property(e => e.SH_NO).HasColumnName("SH_NO").HasMaxLength(100);
                entity.Property(e => e.SH_DD).HasColumnName("SH_DD");
                entity.Property(e => e.BIL_NO).HasColumnName("BIL_NO").HasMaxLength(100);
                entity.Property(e => e.BIL_ID).HasColumnName("BIL_ID").HasMaxLength(20);
                entity.Property(e => e.BIL_TYPE).HasColumnName("BIL_TYPE").HasMaxLength(10);
                entity.Property(e => e.TYPE_ID).HasColumnName("TYPE_ID").HasMaxLength(10);
                entity.Property(e => e.CUS_NO).HasColumnName("CUS_NO").HasMaxLength(30);
                entity.Property(e => e.CUS_NAME).HasColumnName("CUS_NAME").HasMaxLength(300);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.WH_ERP).HasColumnName("WH_ERP").HasMaxLength(30);
                entity.Property(e => e.CON_NO).HasColumnName("CON_NO").HasMaxLength(30);
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.SAL_NO).HasColumnName("SAL_NO").HasMaxLength(30);
                entity.Property(e => e.AREA_SH).HasColumnName("AREA_SH").HasMaxLength(50);
                entity.Property(e => e.REM).HasColumnName("REM");
                entity.Property(e => e.FLAG_JY).HasColumnName("FLAG_JY").HasMaxLength(1);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
                entity.Property(e => e.MODIFY_MAN).HasColumnName("MODIFY_MAN").HasMaxLength(30);
                entity.Property(e => e.MODIFY_DD).HasColumnName("MODIFY_DD");
            });

            // 收货单表身（TF_SH）
            modelBuilder.Entity<TfSh>(entity =>
            {
                entity.ToTable("TF_SH");
                entity.HasKey(e => new { e.SH_NO, e.ITM });
                entity.Property(e => e.SH_NO).HasColumnName("SH_NO").HasMaxLength(100);
                entity.Property(e => e.ITM).HasColumnName("ITM");
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.PRD_NAME).HasColumnName("PRD_NAME").HasMaxLength(320);
                entity.Property(e => e.PRD_MARK).HasColumnName("PRD_MARK").HasMaxLength(255);
                entity.Property(e => e.QTY).HasColumnName("QTY");
                entity.Property(e => e.QTY1).HasColumnName("QTY1");
                entity.Property(e => e.UNIT).HasColumnName("UNIT").HasMaxLength(10);
                entity.Property(e => e.BAT_NO).HasColumnName("BAT_NO").HasMaxLength(40);
                entity.Property(e => e.JY_FLAG).HasColumnName("JY_FLAG").HasMaxLength(1);
                entity.Property(e => e.BIL_NO).HasColumnName("BIL_NO").HasMaxLength(100);
                entity.Property(e => e.BIL_ID).HasColumnName("BIL_ID").HasMaxLength(20);
                entity.Property(e => e.BIL_ITM).HasColumnName("BIL_ITM");
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.SC_DD).HasColumnName("SC_DD");
                entity.Property(e => e.VALID_DD).HasColumnName("VALID_DD");
                entity.Property(e => e.SH_DD).HasColumnName("SH_DD");
                entity.Property(e => e.REM).HasColumnName("REM");

                // 关联关系：表身 -> 表头
                entity.HasOne<MfSh>()
                      .WithMany()
                      .HasForeignKey(e => e.SH_NO)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // 入库单表头（MF_RK）
            modelBuilder.Entity<MfRk>(entity =>
            {
                entity.ToTable("MF_RK");
                entity.HasKey(e => e.RK_NO);
                entity.Property(e => e.RK_NO).HasColumnName("RK_NO").HasMaxLength(100);
                entity.Property(e => e.RK_DD).HasColumnName("RK_DD");
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.CUS_NO).HasColumnName("CUS_NO").HasMaxLength(30);
                entity.Property(e => e.CUS_NAME).HasColumnName("CUS_NAME").HasMaxLength(300);
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.SAL_NO).HasColumnName("SAL_NO").HasMaxLength(30);
                entity.Property(e => e.TYPE_ID).HasColumnName("TYPE_ID").HasMaxLength(10);
                entity.Property(e => e.ERP_AP_NO).HasColumnName("ERP_AP_NO").HasMaxLength(100);
                entity.Property(e => e.BIL_TYPE).HasColumnName("BIL_TYPE").HasMaxLength(10);
                entity.Property(e => e.BIL_ID).HasColumnName("BIL_ID").HasMaxLength(20);
                entity.Property(e => e.BIL_NO).HasColumnName("BIL_NO").HasMaxLength(100);
                entity.Property(e => e.CON_NO).HasColumnName("CON_NO").HasMaxLength(30);
                entity.Property(e => e.REM).HasColumnName("REM");
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
                entity.Property(e => e.MODIFY_MAN).HasColumnName("MODIFY_MAN").HasMaxLength(30);
                entity.Property(e => e.MODIFY_DD).HasColumnName("MODIFY_DD");
                entity.Property(e => e.STATUS_JY).HasColumnName("STATUS_JY").HasMaxLength(10);
                entity.Property(e => e.QC_FLAG).HasColumnName("QC_FLAG").HasMaxLength(1);
                entity.Property(e => e.WMS_ID).HasColumnName("WMS_ID").HasMaxLength(50);
                entity.Property(e => e.TZ_NO_UO).HasColumnName("TZ_NO_UO").HasMaxLength(100);
            });

            // 入库单表身（TF_RK）
            modelBuilder.Entity<TfRk>(entity =>
            {
                entity.ToTable("TF_RK");
                entity.HasKey(e => new { e.RK_NO, e.ITM });
                entity.Property(e => e.RK_NO).HasColumnName("RK_NO").HasMaxLength(100);
                entity.Property(e => e.ITM).HasColumnName("ITM");
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.PRD_NAME).HasColumnName("PRD_NAME").HasMaxLength(320);
                entity.Property(e => e.PRD_MARK).HasColumnName("PRD_MARK").HasMaxLength(255);
                entity.Property(e => e.BAT_NO).HasColumnName("BAT_NO").HasMaxLength(40);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.CHUW).HasColumnName("CHUW").HasMaxLength(30);
                entity.Property(e => e.UNIT).HasColumnName("UNIT").HasMaxLength(10);
                entity.Property(e => e.QTY).HasColumnName("QTY");
                entity.Property(e => e.QTY1).HasColumnName("QTY1");
                entity.Property(e => e.QTY_LOST).HasColumnName("QTY_LOST");
                entity.Property(e => e.BUS_NO).HasColumnName("BUS_NO").HasMaxLength(100);
                entity.Property(e => e.RK_FLOW).HasColumnName("RK_FLOW").HasMaxLength(20);
                entity.Property(e => e.SC_DD).HasColumnName("SC_DD");
                entity.Property(e => e.VALID_DD).HasColumnName("VALID_DD");
                entity.Property(e => e.REM).HasColumnName("REM");

                // 关联关系：表身 -> 表头
                entity.HasOne<MfRk>()
                      .WithMany()
                      .HasForeignKey(e => e.RK_NO)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // 关账作业表（CON_CLOSE）
            modelBuilder.Entity<ConClose>(entity =>
            {
                entity.ToTable("CON_CLOSE");
                entity.HasKey(e => e.GUID);
                entity.Property(e => e.GUID).HasColumnName("GUID").HasMaxLength(50);
                entity.Property(e => e.ACT_NO).HasColumnName("ACT_NO").HasMaxLength(50);
                entity.Property(e => e.CLOSE_DD).HasColumnName("CLOSE_DD");
                entity.Property(e => e.CON_NO).HasColumnName("CON_NO").HasMaxLength(30);
                entity.Property(e => e.MODIFY_MAN).HasColumnName("MODIFY_MAN").HasMaxLength(30);
                entity.Property(e => e.MODIFY_DD).HasColumnName("MODIFY_DD");
            });

            // 出库通知单附属信息（TF_CKTZ_RCV）
            modelBuilder.Entity<TfCktzRcv>(entity =>
            {
                entity.ToTable("TF_CKTZ_RCV");
                entity.HasKey(e => e.TZ_NO);
                entity.Property(e => e.TZ_NO).HasColumnName("TZ_NO").HasMaxLength(100);
                entity.Property(e => e.ADR).HasColumnName("ADR");
                entity.Property(e => e.CELL_NO).HasColumnName("CELL_NO").HasMaxLength(50);
                entity.Property(e => e.CON_MAN).HasColumnName("CON_MAN").HasMaxLength(100);
                entity.Property(e => e.COT_ID).HasColumnName("COT_ID").HasMaxLength(50);
                entity.Property(e => e.COUN_ID).HasColumnName("COUN_ID").HasMaxLength(200);
                entity.Property(e => e.CUS_NAME).HasColumnName("CUS_NAME").HasMaxLength(300);
                entity.Property(e => e.CUS_NO).HasColumnName("CUS_NO").HasMaxLength(30);
                entity.Property(e => e.FH_NO).HasColumnName("FH_NO").HasMaxLength(100);
                entity.Property(e => e.ZIP).HasColumnName("ZIP").HasMaxLength(20);
            });

            // 出库单单据附属信息（TF_CK_RCV）
            modelBuilder.Entity<TfCkRcv>(entity =>
            {
                entity.ToTable("TF_CK_RCV");
                entity.HasKey(e => e.CK_NO);
                entity.Property(e => e.CK_NO).HasColumnName("CK_NO").HasMaxLength(100);
                entity.Property(e => e.ADR).HasColumnName("ADR");
                entity.Property(e => e.CELL_NO).HasColumnName("CELL_NO").HasMaxLength(50);
                entity.Property(e => e.CON_MAN).HasColumnName("CON_MAN").HasMaxLength(100);
                entity.Property(e => e.COT_ID).HasColumnName("COT_ID").HasMaxLength(50);
                entity.Property(e => e.COUN_ID).HasColumnName("COUN_ID").HasMaxLength(200);
                entity.Property(e => e.CUS_NAME).HasColumnName("CUS_NAME").HasMaxLength(300);
                entity.Property(e => e.CUS_NO).HasColumnName("CUS_NO").HasMaxLength(30);
                entity.Property(e => e.FH_NO).HasColumnName("FH_NO").HasMaxLength(100);
                entity.Property(e => e.ZIP).HasColumnName("ZIP").HasMaxLength(20);
            });

            // 单据条码明细（PDA_BAR_COLLECT）
            modelBuilder.Entity<PdaBarCollect>(entity =>
            {
                entity.ToTable("PDA_BAR_COLLECT");
                entity.HasKey(e => e.UNIQUE_ID);
                entity.Property(e => e.UNIQUE_ID).HasColumnName("UNIQUE_ID");
                entity.Property(e => e.BIL_ID).HasColumnName("BIL_ID").HasMaxLength(20);
                entity.Property(e => e.BIL_NO).HasColumnName("BIL_NO").HasMaxLength(100);
                entity.Property(e => e.BIL_ITM).HasColumnName("BIL_ITM");
                entity.Property(e => e.BIL_TYPE).HasColumnName("BIL_TYPE").HasMaxLength(10);
                entity.Property(e => e.BIL_DD).HasColumnName("BIL_DD");
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.PRD_NAME).HasColumnName("PRD_NAME").HasMaxLength(320);
                entity.Property(e => e.BAT_NO).HasColumnName("BAT_NO").HasMaxLength(40);
                entity.Property(e => e.QTY_ORIG).HasColumnName("QTY_ORIG");
                entity.Property(e => e.QTY_SCAN).HasColumnName("QTY_SCAN");
                entity.Property(e => e.WH1).HasColumnName("WH1").HasMaxLength(30);
                entity.Property(e => e.WH2).HasColumnName("WH2").HasMaxLength(30);
                entity.Property(e => e.CHUW1).HasColumnName("CHUW1").HasMaxLength(30);
                entity.Property(e => e.CHUW2).HasColumnName("CHUW2").HasMaxLength(30);
                entity.Property(e => e.BAR_CODE).HasColumnName("BAR_CODE").HasMaxLength(200);
                entity.Property(e => e.BAR_TYPE).HasColumnName("BAR_TYPE").HasMaxLength(20);
                entity.Property(e => e.BOX_NO).HasColumnName("BOX_NO").HasMaxLength(100);
                entity.Property(e => e.CONTAIN_CODE).HasColumnName("CONTAIN_CODE").HasMaxLength(100);
                entity.Property(e => e.CAR_NO).HasColumnName("CAR_NO").HasMaxLength(30);
                entity.Property(e => e.CUS_NO).HasColumnName("CUS_NO").HasMaxLength(30);
                entity.Property(e => e.CUS_NAME).HasColumnName("CUS_NAME").HasMaxLength(300);
                entity.Property(e => e.CON_NO).HasColumnName("CON_NO").HasMaxLength(30);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
            });

            // 仓库库存结余表（SPRD）
            modelBuilder.Entity<Sprd>(entity =>
            {
                entity.ToTable("SPRD");
                entity.HasKey(e => new { e.WH, e.YY, e.MM, e.PRD_NO, e.BAT_NO });
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.YY).HasColumnName("YY");
                entity.Property(e => e.MM).HasColumnName("MM");
                entity.Property(e => e.CON_NO).HasColumnName("CON_NO").HasMaxLength(30);
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.PRD_MARK).HasColumnName("PRD_MARK").HasMaxLength(255);
                entity.Property(e => e.BAT_NO).HasColumnName("BAT_NO").HasMaxLength(40);
                entity.Property(e => e.VALID_DD).HasColumnName("VALID_DD");
                entity.Property(e => e.QTY_IN).HasColumnName("QTY_IN");
                entity.Property(e => e.QTY_OUT).HasColumnName("QTY_OUT");
                entity.Property(e => e.QTY1_IN).HasColumnName("QTY1_IN");
                entity.Property(e => e.QTY1_OUT).HasColumnName("QTY1_OUT");
                entity.Property(e => e.LST_IND).HasColumnName("LST_IND");
                entity.Property(e => e.LST_OTD).HasColumnName("LST_OTD");
            });

            // 储位库存结余表（SPRD_CW）
            modelBuilder.Entity<SprdCw>(entity =>
            {
                entity.ToTable("SPRD_CW");
                entity.HasKey(e => new { e.WH, e.YY, e.MM, e.CHUW, e.PRD_NO, e.BAT_NO });
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.YY).HasColumnName("YY");
                entity.Property(e => e.MM).HasColumnName("MM");
                entity.Property(e => e.CHUW).HasColumnName("CHUW").HasMaxLength(30);
                entity.Property(e => e.CON_NO).HasColumnName("CON_NO").HasMaxLength(30);
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.PRD_MARK).HasColumnName("PRD_MARK").HasMaxLength(255);
                entity.Property(e => e.BAT_NO).HasColumnName("BAT_NO").HasMaxLength(40);
                entity.Property(e => e.VALID_DD).HasColumnName("VALID_DD");
                entity.Property(e => e.QTY_IN).HasColumnName("QTY_IN");
                entity.Property(e => e.QTY_OUT).HasColumnName("QTY_OUT");
                entity.Property(e => e.QTY1_IN).HasColumnName("QTY1_IN");
                entity.Property(e => e.QTY1_OUT).HasColumnName("QTY1_OUT");
                entity.Property(e => e.LST_IND).HasColumnName("LST_IND");
                entity.Property(e => e.LST_OTD).HasColumnName("LST_OTD");
            });

            // 仓库库存表（PRDT1）
            modelBuilder.Entity<Prdt1>(entity =>
            {
                entity.ToTable("PRDT1");
                entity.HasKey(e => new { e.WH, e.PRD_NO, e.BAT_NO });
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.CON_NO).HasColumnName("CON_NO").HasMaxLength(30);
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.PRD_MARK).HasColumnName("PRD_MARK").HasMaxLength(255);
                entity.Property(e => e.BAT_NO).HasColumnName("BAT_NO").HasMaxLength(40);
                entity.Property(e => e.VALID_DD).HasColumnName("VALID_DD");
                entity.Property(e => e.FIELD_ZS).HasColumnName("FIELD_ZS");
                entity.Property(e => e.QTY_IN).HasColumnName("QTY_IN");
                entity.Property(e => e.QTY_OUT).HasColumnName("QTY_OUT");
                entity.Property(e => e.QTY_PK).HasColumnName("QTY_PK");
                entity.Property(e => e.QTY_TY).HasColumnName("QTY_TY");
                entity.Property(e => e.QTY_QC).HasColumnName("QTY_QC");
                entity.Property(e => e.QTY_UO).HasColumnName("QTY_UO");
                entity.Property(e => e.QTY_UP).HasColumnName("QTY_UP");
                entity.Property(e => e.QTY_BC).HasColumnName("QTY_BC");
                entity.Property(e => e.QTY1_IN).HasColumnName("QTY1_IN");
                entity.Property(e => e.QTY1_OUT).HasColumnName("QTY1_OUT");
                entity.Property(e => e.QTY1_PK).HasColumnName("QTY1_PK");
                entity.Property(e => e.LOCK_ID).HasColumnName("LOCK_ID").HasMaxLength(1);
                entity.Property(e => e.LST_IND).HasColumnName("LST_IND");
                entity.Property(e => e.LST_OTD).HasColumnName("LST_OTD");
                entity.Property(e => e.LST_TYD).HasColumnName("LST_TYD");
                entity.Property(e => e.INSERT_DD).HasColumnName("INSERT_DD");
            });

            // 储位库存表（PRDT1_CW）
            modelBuilder.Entity<Prdt1Cw>(entity =>
            {
                entity.ToTable("PRDT1_CW");
                entity.HasKey(e => new { e.WH, e.CHUW, e.PRD_NO, e.BAT_NO });
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.CHUW).HasColumnName("CHUW").HasMaxLength(30);
                entity.Property(e => e.CON_NO).HasColumnName("CON_NO").HasMaxLength(30);
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.PRD_MARK).HasColumnName("PRD_MARK").HasMaxLength(255);
                entity.Property(e => e.BAT_NO).HasColumnName("BAT_NO").HasMaxLength(40);
                entity.Property(e => e.VALID_DD).HasColumnName("VALID_DD");
                entity.Property(e => e.FIELD_ZS).HasColumnName("FIELD_ZS");
                entity.Property(e => e.QTY_IN).HasColumnName("QTY_IN");
                entity.Property(e => e.QTY_OUT).HasColumnName("QTY_OUT");
                entity.Property(e => e.QTY_PK).HasColumnName("QTY_PK");
                entity.Property(e => e.QTY_TY).HasColumnName("QTY_TY");
                entity.Property(e => e.QTY_BC).HasColumnName("QTY_BC");
                entity.Property(e => e.QTY1_IN).HasColumnName("QTY1_IN");
                entity.Property(e => e.QTY1_OUT).HasColumnName("QTY1_OUT");
                entity.Property(e => e.QTY1_PK).HasColumnName("QTY1_PK");
                entity.Property(e => e.QTY1_TY).HasColumnName("QTY1_TY");
                entity.Property(e => e.LST_IND).HasColumnName("LST_IND");
                entity.Property(e => e.LST_OTD).HasColumnName("LST_OTD");
                entity.Property(e => e.LST_TYD).HasColumnName("LST_TYD");
                entity.Property(e => e.INSERT_DD).HasColumnName("INSERT_DD");
            });

            // 货品库存锁定表（PRDT1_LOCK）
            modelBuilder.Entity<Prdt1Lock>(entity =>
            {
                entity.ToTable("PRDT1_LOCK");
                entity.HasKey(e => e.GUID);
                entity.Property(e => e.GUID).HasColumnName("GUID").HasMaxLength(50);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.CON_NO).HasColumnName("CON_NO").HasMaxLength(30);
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.PRD_MARK).HasColumnName("PRD_MARK").HasMaxLength(255);
                entity.Property(e => e.BAT_NO).HasColumnName("BAT_NO").HasMaxLength(40);
                entity.Property(e => e.ACT_NO).HasColumnName("ACT_NO").HasMaxLength(50);
                entity.Property(e => e.LOCK_DD).HasColumnName("LOCK_DD");
            });

            // 批号有效期修改历史（VALIDDD_UPD_HIS）
            modelBuilder.Entity<ValidddUpdHis>(entity =>
            {
                entity.ToTable("VALIDDD_UPD_HIS");
                entity.HasKey(e => e.HIS_NO);
                entity.Property(e => e.BAT_NO).HasColumnName("BAT_NO").HasMaxLength(40);
                entity.Property(e => e.CON_NO).HasColumnName("CON_NO").HasMaxLength(30);
                entity.Property(e => e.HIS_NO).HasColumnName("HIS_NO");
                entity.Property(e => e.PRD_MARK).HasColumnName("PRD_MARK").HasMaxLength(255);
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.TASK_NO).HasColumnName("TASK_NO").HasMaxLength(50);
                entity.Property(e => e.UPD_DATE).HasColumnName("UPD_DATE");
                entity.Property(e => e.UP_USER).HasColumnName("UP_USER").HasMaxLength(30);
                entity.Property(e => e.VALID_DD_CUR).HasColumnName("VALID_DD_CUR");
                entity.Property(e => e.VALID_DD_ORG).HasColumnName("VALID_DD_ORG");
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
            });

            // 不合格原因设定（SPC_SET）
            modelBuilder.Entity<SpcSet>(entity =>
            {
                entity.ToTable("SPC_SET");
                entity.HasKey(e => e.SPC_NO);
                entity.Property(e => e.SPC_NO).HasColumnName("SPC_NO").HasMaxLength(50);
                entity.Property(e => e.NAME).HasColumnName("NAME").HasMaxLength(200);
                entity.Property(e => e.SPC_UP).HasColumnName("SPC_UP").HasMaxLength(50);
                entity.Property(e => e.REM).HasColumnName("REM").HasMaxLength(500);
                entity.Property(e => e.TP_ID).HasColumnName("TP_ID").HasMaxLength(10);
            });

            // 车间入库单表头（MF_CJ）
            modelBuilder.Entity<MfCj>(entity =>
            {
                entity.ToTable("MF_CJ");
                entity.HasKey(e => e.CJ_NO);
                entity.Property(e => e.CJ_NO).HasColumnName("CJ_NO").HasMaxLength(100);
                entity.Property(e => e.CJ_DD).HasColumnName("CJ_DD");
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.SAL_NO).HasColumnName("SAL_NO").HasMaxLength(30);
                entity.Property(e => e.FLAG_JY).HasColumnName("FLAG_JY").HasMaxLength(1);
                entity.Property(e => e.TYPE_ID).HasColumnName("TYPE_ID").HasMaxLength(10);
                entity.Property(e => e.BIL_TYPE).HasColumnName("BIL_TYPE").HasMaxLength(10);
                entity.Property(e => e.REM).HasColumnName("REM");
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
                entity.Property(e => e.MODIFY_MAN).HasColumnName("MODIFY_MAN").HasMaxLength(30);
                entity.Property(e => e.MODIFY_DD).HasColumnName("MODIFY_DD");
                entity.Property(e => e.TY_PUSH_SW).HasColumnName("TY_PUSH_SW").HasMaxLength(1);
                entity.Property(e => e.TY_SYS).HasColumnName("TY_SYS").HasMaxLength(10);
                entity.Property(e => e.RK_NO).HasColumnName("RK_NO").HasMaxLength(100);
                entity.Property(e => e.TZ_NO).HasColumnName("TZ_NO").HasMaxLength(100);
            });

            // 车间入库单表身（TF_CJ）
            modelBuilder.Entity<TfCj>(entity =>
            {
                entity.ToTable("TF_CJ");
                entity.HasKey(e => new { e.CJ_NO, e.ITM });
                entity.Property(e => e.CJ_NO).HasColumnName("CJ_NO").HasMaxLength(100);
                entity.Property(e => e.ITM).HasColumnName("ITM");
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.PRD_NAME).HasColumnName("PRD_NAME").HasMaxLength(320);
                entity.Property(e => e.QTY).HasColumnName("QTY");
                entity.Property(e => e.QTY1).HasColumnName("QTY1");
                entity.Property(e => e.UNIT).HasColumnName("UNIT").HasMaxLength(10);
                entity.Property(e => e.BAT_NO).HasColumnName("BAT_NO").HasMaxLength(40);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.REM).HasColumnName("REM");

                // 关联关系：表身 -> 表头
                entity.HasOne<MfCj>()
                      .WithMany()
                      .HasForeignKey(e => e.CJ_NO)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ========== 三级菜单界面 - 新增表Fluent API配置开始 ==========
            
            // 角色定义（ROLE）
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLE");
                entity.HasKey(e => e.ROLENO);
                entity.Property(e => e.ROLENO).HasColumnName("ROLENO").HasMaxLength(30);
                entity.Property(e => e.NAME).HasColumnName("NAME").HasMaxLength(200);
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.DEPRO_NO).HasColumnName("DEPRO_NO").HasMaxLength(30);
                entity.Property(e => e.COMPNO).HasColumnName("COMPNO").HasMaxLength(30);
                entity.Property(e => e.TYPE_ID).HasColumnName("TYPE_ID").HasMaxLength(20);
                entity.Property(e => e.PUBLIC_ID).HasColumnName("PUBLIC_ID").HasMaxLength(1);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.REM).HasColumnName("REM");
                entity.Property(e => e.SUB_ID).HasColumnName("SUB_ID").HasMaxLength(1);
            });

            // ONLINE角色权限（FX_PSWD）
            modelBuilder.Entity<FxPswd>(entity =>
            {
                entity.ToTable("FX_PSWD");
                entity.HasKey(e => new { e.ROLENO, e.PGM });
                entity.Property(e => e.ROLENO).HasColumnName("ROLENO").HasMaxLength(30);
                entity.Property(e => e.PGM).HasColumnName("PGM").HasMaxLength(50);
                entity.Property(e => e.TYPE_ID).HasColumnName("TYPE_ID").HasMaxLength(20);
                entity.Property(e => e.COMPNO).HasColumnName("COMPNO").HasMaxLength(30);
                entity.Property(e => e.DEPRO_NO).HasColumnName("DEPRO_NO").HasMaxLength(30);
                entity.Property(e => e.QRY).HasColumnName("QRY").HasMaxLength(1);
                entity.Property(e => e.INS).HasColumnName("INS").HasMaxLength(1);
                entity.Property(e => e.UPD).HasColumnName("UPD").HasMaxLength(1);
                entity.Property(e => e.DEL).HasColumnName("DEL").HasMaxLength(1);
                entity.Property(e => e.PRN).HasColumnName("PRN").HasMaxLength(1);
                entity.Property(e => e.ALLOW_ID).HasColumnName("ALLOW_ID").HasMaxLength(1);
            });

            // ONLINE角色所属用户（PSWD_ROLE）
            modelBuilder.Entity<PswdRole>(entity =>
            {
                entity.ToTable("PSWD_ROLE");
                entity.HasKey(e => new { e.COMPNO, e.ROLENO, e.TYPE_ID, e.USR });
                entity.Property(e => e.COMPNO).HasColumnName("COMPNO").HasMaxLength(30);
                entity.Property(e => e.ROLENO).HasColumnName("ROLENO").HasMaxLength(30);
                entity.Property(e => e.TYPE_ID).HasColumnName("TYPE_ID").HasMaxLength(20);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
            });

            // 条形码属性设定档（BAR_PSWD_PROP）
            modelBuilder.Entity<BarPswdProp>(entity =>
            {
                entity.ToTable("BAR_PSWD_PROP");
                entity.HasKey(e => new { e.PGM, e.ROLENO, e.TYPE_ID, e.FLD_NAME });
                entity.Property(e => e.COMPNO).HasColumnName("COMPNO").HasMaxLength(30);
                entity.Property(e => e.ROLENO).HasColumnName("ROLENO").HasMaxLength(30);
                entity.Property(e => e.TYPE_ID).HasColumnName("TYPE_ID").HasMaxLength(20);
                entity.Property(e => e.PGM).HasColumnName("PGM").HasMaxLength(50);
                entity.Property(e => e.FLD_NAME).HasColumnName("FLD_NAME").HasMaxLength(100);
                entity.Property(e => e.FLD_VALUE).HasColumnName("FLD_VALUE").HasMaxLength(500);
                entity.Property(e => e.REM).HasColumnName("REM").HasMaxLength(500);
            });

            // 报表类型设定表头（MF_RPT）
            modelBuilder.Entity<MfRpt>(entity =>
            {
                entity.ToTable("MF_RPT");
                entity.HasKey(e => new { e.PGM, e.ITM });
                entity.Property(e => e.PGM).HasColumnName("PGM").HasMaxLength(50);
                entity.Property(e => e.ITM).HasColumnName("ITM");
                entity.Property(e => e.NAME_GB).HasColumnName("NAME_GB").HasMaxLength(200);
                entity.Property(e => e.NAME_BIG5).HasColumnName("NAME_BIG5").HasMaxLength(200);
                entity.Property(e => e.NAME_ENG).HasColumnName("NAME_ENG").HasMaxLength(200);
                entity.Property(e => e.CHK_MENU).HasColumnName("CHK_MENU").HasMaxLength(1);
                entity.Property(e => e.CHK_USRS).HasColumnName("CHK_USRS").HasMaxLength(500);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
            });

            // 报表类型设定表身（TF_RPT）
            modelBuilder.Entity<TfRpt>(entity =>
            {
                entity.ToTable("TF_RPT");
                entity.HasKey(e => new { e.PGM, e.ITM, e.FLD_INDEX });
                entity.Property(e => e.PGM).HasColumnName("PGM").HasMaxLength(50);
                entity.Property(e => e.ITM).HasColumnName("ITM");
                entity.Property(e => e.FLD_INDEX).HasColumnName("FLD_INDEX");
                entity.Property(e => e.FLD_NAME).HasColumnName("FLD_NAME").HasMaxLength(100);
                entity.Property(e => e.FLD_LEN).HasColumnName("FLD_LEN");
                entity.Property(e => e.FLD_STYLE).HasColumnName("FLD_STYLE").HasMaxLength(50);
                entity.Property(e => e.FLD_PARA).HasColumnName("FLD_PARA").HasMaxLength(200);
                entity.Property(e => e.CHK_COND).HasColumnName("CHK_COND").HasMaxLength(1);
                entity.Property(e => e.CHK_DISP).HasColumnName("CHK_DISP").HasMaxLength(1);
                entity.Property(e => e.CHK_SUM).HasColumnName("CHK_SUM").HasMaxLength(1);
                entity.Property(e => e.CHK_STATS).HasColumnName("CHK_STATS").HasMaxLength(1);
                entity.Property(e => e.COND_TYPE).HasColumnName("COND_TYPE").HasMaxLength(20);
                entity.Property(e => e.SUM_TYPE).HasColumnName("SUM_TYPE").HasMaxLength(20);
                entity.Property(e => e.REM_GB).HasColumnName("REM_GB").HasMaxLength(100);
                entity.Property(e => e.REM_BIG5).HasColumnName("REM_BIG5").HasMaxLength(100);
                entity.Property(e => e.REM_ENG).HasColumnName("REM_ENG").HasMaxLength(100);

                entity.HasOne<MfRpt>().WithMany().HasForeignKey(e => new { e.PGM, e.ITM }).OnDelete(DeleteBehavior.Restrict);
            });

            // 自定义报表表头（MF_QRY）
            modelBuilder.Entity<MfQry>(entity =>
            {
                entity.ToTable("MF_QRY");
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).HasColumnName("ID").HasMaxLength(50);
                entity.Property(e => e.NAME).HasColumnName("NAME").HasMaxLength(200);
                entity.Property(e => e.SQL).HasColumnName("SQL");
                entity.Property(e => e.USRS).HasColumnName("USRS").HasMaxLength(500);
                entity.Property(e => e.CHK_MAN).HasColumnName("CHK_MAN").HasMaxLength(30);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
                entity.Property(e => e.CLS_DATE).HasColumnName("CLS_DATE");
            });

            // 自定义报表表身（TF_QRY）
            modelBuilder.Entity<TfQry>(entity =>
            {
                entity.ToTable("TF_QRY");
                entity.HasKey(e => new { e.ID, e.FLD_NAME });
                entity.Property(e => e.ID).HasColumnName("ID").HasMaxLength(50);
                entity.Property(e => e.FLD_NAME).HasColumnName("FLD_NAME").HasMaxLength(100);
                entity.Property(e => e.FLD_TYPE).HasColumnName("FLD_TYPE").HasMaxLength(50);
                entity.Property(e => e.REM_GB).HasColumnName("REM_GB").HasMaxLength(200);
                entity.Property(e => e.REM_BIG5).HasColumnName("REM_BIG5").HasMaxLength(200);
                entity.Property(e => e.REM_ENG).HasColumnName("REM_ENG").HasMaxLength(200);

                entity.HasOne<MfQry>().WithMany().HasForeignKey(e => e.ID).OnDelete(DeleteBehavior.Restrict);
            });

            // 仿真布局设备设定（EMULATE_SET）
            modelBuilder.Entity<EmulateSet>(entity =>
            {
                entity.ToTable("EMULATE_SET");
                entity.HasKey(e => e.EMULATE_ID);
                entity.Property(e => e.EMULATE_ID).HasColumnName("EMULATE_ID");
                entity.Property(e => e.DEVICE_ID).HasColumnName("DEVICE_ID").HasMaxLength(50);
                entity.Property(e => e.TYPE_ID).HasColumnName("TYPE_ID").HasMaxLength(10);
                entity.Property(e => e.STATUS_ID).HasColumnName("STATUS_ID").HasMaxLength(10);
                entity.Property(e => e.MODIFY_DD).HasColumnName("MODIFY_DD");
            });

            // 仓储可视化图（MY_WH_VIEW）
            modelBuilder.Entity<MyWhView>(entity =>
            {
                entity.ToTable("MY_WH_VIEW");
                entity.HasKey(e => e.VW_NO);
                entity.Property(e => e.VW_NO).HasColumnName("VW_NO").HasMaxLength(30);
                entity.Property(e => e.NAME).HasColumnName("NAME").HasMaxLength(200);
                entity.Property(e => e.STOP_ID).HasColumnName("STOP_ID").HasMaxLength(1);
                entity.Property(e => e.SYS_ID).HasColumnName("SYS_ID").HasMaxLength(1);
                entity.Property(e => e.SVG_CONTENT).HasColumnName("SVG_CONTENT");
                entity.Property(e => e.CHK_USRS).HasColumnName("CHK_USRS").HasMaxLength(500);
            });

            // 自动服务执行日志（SVC_LOG）
            modelBuilder.Entity<SvcLog>(entity =>
            {
                entity.ToTable("SVC_LOG");
                entity.HasKey(e => e.SVC_NO);
                entity.Property(e => e.SVC_NO).HasColumnName("SVC_NO").HasMaxLength(50);
                entity.Property(e => e.SVC_NO1).HasColumnName("SVC_NO1").HasMaxLength(50);
                entity.Property(e => e.NAME).HasColumnName("NAME").HasMaxLength(200);
                entity.Property(e => e.NAME1).HasColumnName("NAME1").HasMaxLength(200);
                entity.Property(e => e.PATH).HasColumnName("PATH").HasMaxLength(500);
                entity.Property(e => e.INTERVAL_TIME).HasColumnName("INTERVAL_TIME");
                entity.Property(e => e.START_TIME).HasColumnName("START_TIME");
                entity.Property(e => e.END_TIME).HasColumnName("END_TIME");
                entity.Property(e => e.REM).HasColumnName("REM").HasMaxLength(500);
            });

            // 自动服务执行异常（SVC_YC）
            modelBuilder.Entity<SvcYc>(entity =>
            {
                entity.ToTable("SVC_YC");
                entity.HasKey(e => e.YC_NO);
                entity.Property(e => e.YC_NO).HasColumnName("YC_NO").HasMaxLength(50);
                entity.Property(e => e.SVC_NO).HasColumnName("SVC_NO").HasMaxLength(50);
                entity.Property(e => e.SVC_NO1).HasColumnName("SVC_NO1").HasMaxLength(50);
                entity.Property(e => e.REM).HasColumnName("REM").HasMaxLength(500);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
            });

            // 入库处理表自动化（LK_ACTION_I）
            modelBuilder.Entity<LkActionI>(entity =>
            {
                entity.ToTable("LK_ACTION_I");
                entity.HasKey(e => e.ACT_NO);
                entity.Property(e => e.ACT_NO).HasColumnName("ACT_NO").HasMaxLength(50);
                entity.Property(e => e.METHOD_NO).HasColumnName("METHOD_NO").HasMaxLength(100);
                entity.Property(e => e.HTTP_METHOD).HasColumnName("HTTP_METHOD").HasMaxLength(10);
                entity.Property(e => e.PATH).HasColumnName("PATH").HasMaxLength(500);
                entity.Property(e => e.CLIENT_IP).HasColumnName("CLIENT_IP").HasMaxLength(50);
                entity.Property(e => e.COMPNO).HasColumnName("COMPNO").HasMaxLength(30);
                entity.Property(e => e.CON_NO).HasColumnName("CON_NO").HasMaxLength(30);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.CHUW).HasColumnName("CHUW").HasMaxLength(30);
                entity.Property(e => e.CONTAIN_CODE).HasColumnName("CONTAIN_CODE").HasMaxLength(100);
                entity.Property(e => e.BIL_ID).HasColumnName("BIL_ID").HasMaxLength(20);
                entity.Property(e => e.BIL_NO).HasColumnName("BIL_NO").HasMaxLength(100);
                entity.Property(e => e.SUP_NO).HasColumnName("SUP_NO").HasMaxLength(30);
                entity.Property(e => e.STATUS_ID).HasColumnName("STATUS_ID").HasMaxLength(10);
                entity.Property(e => e.ERR_CODE).HasColumnName("ERR_CODE").HasMaxLength(50);
                entity.Property(e => e.ERR_MSG).HasColumnName("ERR_MSG").HasMaxLength(500);
                entity.Property(e => e.JSON_CONTENT).HasColumnName("JSON_CONTENT");
                entity.Property(e => e.REQUEST_SIZE).HasColumnName("REQUEST_SIZE");
                entity.Property(e => e.RESULT_SIZE).HasColumnName("RESULT_SIZE");
                entity.Property(e => e.START_DATE).HasColumnName("START_DATE");
                entity.Property(e => e.END_DATE).HasColumnName("END_DATE");
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
                entity.Property(e => e.WH2).HasColumnName("WH2").HasMaxLength(30);
                entity.Property(e => e.CHUW2).HasColumnName("CHUW2").HasMaxLength(30);
            });

            // 出库处理表自动化（LK_ACTION_O）
            modelBuilder.Entity<LkActionO>(entity =>
            {
                entity.ToTable("LK_ACTION_O");
                entity.HasKey(e => e.ACT_NO);
                entity.Property(e => e.ACT_NO).HasColumnName("ACT_NO").HasMaxLength(50);
                entity.Property(e => e.METHOD_NO).HasColumnName("METHOD_NO").HasMaxLength(100);
                entity.Property(e => e.HTTP_METHOD).HasColumnName("HTTP_METHOD").HasMaxLength(10);
                entity.Property(e => e.PATH).HasColumnName("PATH").HasMaxLength(500);
                entity.Property(e => e.COMPNO).HasColumnName("COMPNO").HasMaxLength(30);
                entity.Property(e => e.CON_NO).HasColumnName("CON_NO").HasMaxLength(30);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.CHUW).HasColumnName("CHUW").HasMaxLength(30);
                entity.Property(e => e.BIL_ID).HasColumnName("BIL_ID").HasMaxLength(20);
                entity.Property(e => e.BIL_NO).HasColumnName("BIL_NO").HasMaxLength(100);
                entity.Property(e => e.SUP_NO).HasColumnName("SUP_NO").HasMaxLength(30);
                entity.Property(e => e.STATUS_ID).HasColumnName("STATUS_ID").HasMaxLength(10);
                entity.Property(e => e.ERR_CODE).HasColumnName("ERR_CODE").HasMaxLength(50);
                entity.Property(e => e.ERR_MSG).HasColumnName("ERR_MSG").HasMaxLength(500);
                entity.Property(e => e.JSON_CONTENT).HasColumnName("JSON_CONTENT");
                entity.Property(e => e.START_DATE).HasColumnName("START_DATE");
                entity.Property(e => e.END_DATE).HasColumnName("END_DATE");
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
            });

            // 接口传入处理表（API_ACTION_I）
            modelBuilder.Entity<ApiActionI>(entity =>
            {
                entity.ToTable("API_ACTION_I");
                entity.HasKey(e => e.ACT_NO);
                entity.Property(e => e.ACT_NO).HasColumnName("ACT_NO").HasMaxLength(50);
                entity.Property(e => e.METHOD_NO).HasColumnName("METHOD_NO").HasMaxLength(100);
                entity.Property(e => e.HTTP_METHOD).HasColumnName("HTTP_METHOD").HasMaxLength(10);
                entity.Property(e => e.PATH).HasColumnName("PATH").HasMaxLength(500);
                entity.Property(e => e.CLIENT_IP).HasColumnName("CLIENT_IP").HasMaxLength(50);
                entity.Property(e => e.COMPNO).HasColumnName("COMPNO").HasMaxLength(30);
                entity.Property(e => e.CON_NO).HasColumnName("CON_NO").HasMaxLength(30);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.CHUW).HasColumnName("CHUW").HasMaxLength(30);
                entity.Property(e => e.CONTAIN_CODE).HasColumnName("CONTAIN_CODE").HasMaxLength(100);
                entity.Property(e => e.BIL_ID).HasColumnName("BIL_ID").HasMaxLength(20);
                entity.Property(e => e.BIL_NO).HasColumnName("BIL_NO").HasMaxLength(100);
                entity.Property(e => e.BIL_TYPE).HasColumnName("BIL_TYPE").HasMaxLength(20);
                entity.Property(e => e.SUP_NO).HasColumnName("SUP_NO").HasMaxLength(30);
                entity.Property(e => e.OTH_BIL_ID).HasColumnName("OTH_BIL_ID").HasMaxLength(100);
                entity.Property(e => e.OTH_BIL_NO).HasColumnName("OTH_BIL_NO").HasMaxLength(100);
                entity.Property(e => e.REF_ID).HasColumnName("REF_ID").HasMaxLength(100);
                entity.Property(e => e.STATUS_ID).HasColumnName("STATUS_ID").HasMaxLength(10);
                entity.Property(e => e.ERR_CODE).HasColumnName("ERR_CODE").HasMaxLength(50);
                entity.Property(e => e.ERR_MSG).HasColumnName("ERR_MSG").HasMaxLength(500);
                entity.Property(e => e.JSON_CONTENT).HasColumnName("JSON_CONTENT");
                entity.Property(e => e.REQUEST_SIZE).HasColumnName("REQUEST_SIZE");
                entity.Property(e => e.RESULT_SIZE).HasColumnName("RESULT_SIZE");
                entity.Property(e => e.START_DATE).HasColumnName("START_DATE");
                entity.Property(e => e.END_DATE).HasColumnName("END_DATE");
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
                entity.Property(e => e.WH2).HasColumnName("WH2").HasMaxLength(30);
                entity.Property(e => e.CHUW2).HasColumnName("CHUW2").HasMaxLength(30);
            });

            // 接口推送处理表（API_ACTION_O）
            modelBuilder.Entity<ApiActionO>(entity =>
            {
                entity.ToTable("API_ACTION_O");
                entity.HasKey(e => e.ACT_NO);
                entity.Property(e => e.ACT_NO).HasColumnName("ACT_NO").HasMaxLength(50);
                entity.Property(e => e.METHOD_NO).HasColumnName("METHOD_NO").HasMaxLength(100);
                entity.Property(e => e.HTTP_METHOD).HasColumnName("HTTP_METHOD").HasMaxLength(10);
                entity.Property(e => e.PATH).HasColumnName("PATH").HasMaxLength(500);
                entity.Property(e => e.COMPNO).HasColumnName("COMPNO").HasMaxLength(30);
                entity.Property(e => e.CON_NO).HasColumnName("CON_NO").HasMaxLength(30);
                entity.Property(e => e.BIL_ID).HasColumnName("BIL_ID").HasMaxLength(20);
                entity.Property(e => e.BIL_NO).HasColumnName("BIL_NO").HasMaxLength(100);
                entity.Property(e => e.BIL_TYPE).HasColumnName("BIL_TYPE").HasMaxLength(20);
                entity.Property(e => e.SUP_NO).HasColumnName("SUP_NO").HasMaxLength(30);
                entity.Property(e => e.REF_ID).HasColumnName("REF_ID").HasMaxLength(100);
                entity.Property(e => e.OTH_BIL_ID).HasColumnName("OTH_BIL_ID").HasMaxLength(100);
                entity.Property(e => e.OTH_BIL_NO).HasColumnName("OTH_BIL_NO").HasMaxLength(100);
                entity.Property(e => e.STATUS_ID).HasColumnName("STATUS_ID").HasMaxLength(10);
                entity.Property(e => e.ERR_CODE).HasColumnName("ERR_CODE").HasMaxLength(50);
                entity.Property(e => e.ERR_HTTP).HasColumnName("ERR_HTTP").HasMaxLength(50);
                entity.Property(e => e.ERR_MSG).HasColumnName("ERR_MSG").HasMaxLength(500);
                entity.Property(e => e.HTTP_CODE).HasColumnName("HTTP_CODE").HasMaxLength(10);
                entity.Property(e => e.JSON_CONTENT).HasColumnName("JSON_CONTENT");
                entity.Property(e => e.PUSH_CONTENT).HasColumnName("PUSH_CONTENT");
                entity.Property(e => e.PUSH_SIZE).HasColumnName("PUSH_SIZE");
                entity.Property(e => e.RESULT_SIZE).HasColumnName("RESULT_SIZE");
                entity.Property(e => e.RUN_COUNT).HasColumnName("RUN_COUNT");
                entity.Property(e => e.START_DATE).HasColumnName("START_DATE");
                entity.Property(e => e.END_DATE).HasColumnName("END_DATE");
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
            });

            // 系统设备表头（HW_SET）
            modelBuilder.Entity<HwSet>(entity =>
            {
                entity.ToTable("HW_SET");
                entity.HasKey(e => e.HW_NO);
                entity.Property(e => e.HW_NO).HasColumnName("HW_NO").HasMaxLength(30);
                entity.Property(e => e.NAME).HasColumnName("NAME").HasMaxLength(200);
                entity.Property(e => e.IP).HasColumnName("IP").HasMaxLength(50);
                entity.Property(e => e.PORT).HasColumnName("PORT").HasMaxLength(10);
                entity.Property(e => e.MODEL_NO).HasColumnName("MODEL_NO").HasMaxLength(30);
                entity.Property(e => e.TYPE_NO).HasColumnName("TYPE_NO").HasMaxLength(30);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.STOP_ID).HasColumnName("STOP_ID").HasMaxLength(1);
            });

            // 系统设备表身（HW_SET_P）
            modelBuilder.Entity<HwSetP>(entity =>
            {
                entity.ToTable("HW_SET_P");
                entity.HasKey(e => new { e.HW_NO, e.PROP_NO });
                entity.Property(e => e.HW_NO).HasColumnName("HW_NO").HasMaxLength(30);
                entity.Property(e => e.PROP_NO).HasColumnName("PROP_NO").HasMaxLength(50);
                entity.Property(e => e.VALUE).HasColumnName("VALUE").HasMaxLength(500);
                entity.Property(e => e.REM).HasColumnName("REM").HasMaxLength(200);

                entity.HasOne<HwSet>().WithMany().HasForeignKey(e => e.HW_NO).OnDelete(DeleteBehavior.Cascade);
            });

            // 接口传入处理日志表（API_ACTION_HIS_I）
            modelBuilder.Entity<ApiActionHisI>(entity =>
            {
                entity.ToTable("API_ACTION_HIS_I");
                entity.HasKey(e => e.HIS_NO);
                entity.Property(e => e.HIS_NO).HasColumnName("HIS_NO");
                entity.Property(e => e.ACT_ID).HasColumnName("ACT_ID").HasMaxLength(20);
                entity.Property(e => e.ACT_NO).HasColumnName("ACT_NO").HasMaxLength(50);
                entity.Property(e => e.ACT_NO_MAIN).HasColumnName("ACT_NO_MAIN").HasMaxLength(50);
                entity.Property(e => e.METHOD_NO).HasColumnName("METHOD_NO").HasMaxLength(100);
                entity.Property(e => e.HTTP_METHOD).HasColumnName("HTTP_METHOD").HasMaxLength(10);
                entity.Property(e => e.PATH).HasColumnName("PATH").HasMaxLength(500);
                entity.Property(e => e.COMPNO).HasColumnName("COMPNO").HasMaxLength(30);
                entity.Property(e => e.CON_NO).HasColumnName("CON_NO").HasMaxLength(30);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.CHUW).HasColumnName("CHUW").HasMaxLength(30);
                entity.Property(e => e.CONTAIN_CODE).HasColumnName("CONTAIN_CODE").HasMaxLength(100);
                entity.Property(e => e.BIL_ID).HasColumnName("BIL_ID").HasMaxLength(20);
                entity.Property(e => e.BIL_NO).HasColumnName("BIL_NO").HasMaxLength(100);
                entity.Property(e => e.BIL_TYPE).HasColumnName("BIL_TYPE").HasMaxLength(20);
                entity.Property(e => e.SUP_NO).HasColumnName("SUP_NO").HasMaxLength(30);
                entity.Property(e => e.OTH_BIL_ID).HasColumnName("OTH_BIL_ID").HasMaxLength(100);
                entity.Property(e => e.OTH_BIL_NO).HasColumnName("OTH_BIL_NO").HasMaxLength(100);
                entity.Property(e => e.REF_ID).HasColumnName("REF_ID").HasMaxLength(100);
                entity.Property(e => e.STATUS_ID).HasColumnName("STATUS_ID").HasMaxLength(10);
                entity.Property(e => e.ERR_CODE).HasColumnName("ERR_CODE").HasMaxLength(50);
                entity.Property(e => e.ERR_MSG).HasColumnName("ERR_MSG").HasMaxLength(500);
                entity.Property(e => e.JSON_CONTENT).HasColumnName("JSON_CONTENT");
                entity.Property(e => e.REQUEST_SIZE).HasColumnName("REQUEST_SIZE");
                entity.Property(e => e.RESULT_SIZE).HasColumnName("RESULT_SIZE");
                entity.Property(e => e.START_DATE).HasColumnName("START_DATE");
                entity.Property(e => e.END_DATE).HasColumnName("END_DATE");
                entity.Property(e => e.WH2).HasColumnName("WH2").HasMaxLength(30);
                entity.Property(e => e.CHUW2).HasColumnName("CHUW2").HasMaxLength(30);
            });

            // 接口推送处理日志表（API_ACTION_HIS_O）
            modelBuilder.Entity<ApiActionHisO>(entity =>
            {
                entity.ToTable("API_ACTION_HIS_O");
                entity.HasKey(e => e.HIS_NO);
                entity.Property(e => e.HIS_NO).HasColumnName("HIS_NO");
                entity.Property(e => e.ACT_ID).HasColumnName("ACT_ID").HasMaxLength(20);
                entity.Property(e => e.ACT_NO).HasColumnName("ACT_NO").HasMaxLength(50);
                entity.Property(e => e.ACT_NO_NEW).HasColumnName("ACT_NO_NEW").HasMaxLength(50);
                entity.Property(e => e.METHOD_NO).HasColumnName("METHOD_NO").HasMaxLength(100);
                entity.Property(e => e.HTTP_METHOD).HasColumnName("HTTP_METHOD").HasMaxLength(10);
                entity.Property(e => e.PATH).HasColumnName("PATH").HasMaxLength(500);
                entity.Property(e => e.COMPNO).HasColumnName("COMPNO").HasMaxLength(30);
                entity.Property(e => e.CON_NO).HasColumnName("CON_NO").HasMaxLength(30);
                entity.Property(e => e.BIL_ID).HasColumnName("BIL_ID").HasMaxLength(20);
                entity.Property(e => e.BIL_NO).HasColumnName("BIL_NO").HasMaxLength(100);
                entity.Property(e => e.SUP_NO).HasColumnName("SUP_NO").HasMaxLength(30);
                entity.Property(e => e.REF_ID).HasColumnName("REF_ID").HasMaxLength(100);
                entity.Property(e => e.OTH_BIL_ID).HasColumnName("OTH_BIL_ID").HasMaxLength(100);
                entity.Property(e => e.OTH_BIL_NO).HasColumnName("OTH_BIL_NO").HasMaxLength(100);
                entity.Property(e => e.STATUS_ID).HasColumnName("STATUS_ID").HasMaxLength(10);
                entity.Property(e => e.ERR_CODE).HasColumnName("ERR_CODE").HasMaxLength(50);
                entity.Property(e => e.ERR_HTTP).HasColumnName("ERR_HTTP").HasMaxLength(50);
                entity.Property(e => e.ERR_MSG).HasColumnName("ERR_MSG").HasMaxLength(500);
                entity.Property(e => e.HTTP_CODE).HasColumnName("HTTP_CODE").HasMaxLength(10);
                entity.Property(e => e.JSON_CONTENT).HasColumnName("JSON_CONTENT");
                entity.Property(e => e.PUSH_CONTENT).HasColumnName("PUSH_CONTENT");
                entity.Property(e => e.PUSH_SIZE).HasColumnName("PUSH_SIZE");
                entity.Property(e => e.RESULT_SIZE).HasColumnName("RESULT_SIZE");
                entity.Property(e => e.START_DATE).HasColumnName("START_DATE");
                entity.Property(e => e.END_DATE).HasColumnName("END_DATE");
            });

            // 拣货退回单表身（TF_JT）
            modelBuilder.Entity<TfJt>(entity =>
            {
                entity.ToTable("TF_JT");
                entity.HasKey(e => new { e.JT_NO, e.ITM });
            });

            // 出库退回通知单表身（TF_CKTB）
            modelBuilder.Entity<TfCktb>(entity =>
            {
                entity.ToTable("TF_CKTB");
                entity.HasKey(e => new { e.TB_NO, e.ITM });
            });

            // 二次分拣单表身（TF_PKFJ）
            modelBuilder.Entity<TfPkfj>(entity =>
            {
                entity.ToTable("TF_PKFJ");
                entity.HasKey(e => new { e.PKFJ_NO, e.ITM });
            });

            // 波次拣货任务单表身（TF_JHRW）
            modelBuilder.Entity<TfJhrw>(entity =>
            {
                entity.ToTable("TF_JHRW");
                entity.HasKey(e => new { e.JR_NO, e.ITM });
            });

            // 出库单表身（TF_CK）
            modelBuilder.Entity<TfCk>(entity =>
            {
                entity.ToTable("TF_CK");
                entity.HasKey(e => new { e.CK_ID, e.ITM });
            });

            // ========== 三级菜单界面 - 新增表Fluent API配置结束 ==========
        }
    }
}
