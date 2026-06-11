# AI-WMS PLUS 账套管理器 - 完整开发计划（V3）

## 一、需求概述

### 1.1 背景
- **wmssystem** 数据库：系统库，每个项目共用，存储用户/权限/账套配置信息
- **db_gz01** 等数据库：账套库，数据库名可变（如 db_gz01、db_gz02），表结构相同，一个项目可有多个账套（测试账套、正式账套等）
- 当前问题：`WarehouseConnection` 硬编码在 `appsettings.json` 中，无法动态切换账套
- **关键点**：每个账套有独立的 WMS 登录用户（存储于 `wmssystem.pswd` 表，COMPNO = 账套代号）
- **登录页选择**：用户在 WMS Web 登录界面可下拉选择要登录的账套，选定后使用该账套对应的数据库和用户体系

### 1.2 目标
开发 **AI-WMS PLUS 账套管理器**（C/S 架构 WinForms 桌面程序）+ 改造 WMS 登录流程，实现：
1. 安装时自动创建 `wmssystem` 系统库及表结构
2. 账套的增删改查管理（含完整的6步向导）
3. 新建账套时同步创建 WMS 登录管理员账号
4. **WMS 登录页显示账套下拉列表**，用户选择账套后再输入用户名密码登录
5. 选择不同账套 → 使用不同账套数据库的数据

---

## 二、整体架构

```
┌──────────────────────────────────────┐       ┌─────────────────────────────┐
│   AI-WMS PLUS 账套管理器 (C/S)        │       │      MySQL Server           │
│   WinForms 桌面应用程序               │──────▶│                             │
│                                     │       │  ┌───────────────────────┐  │
│   功能模块:                          │       │  │  wmssystem (系统库)     │  │
│   ├─ 账套列表管理                     │       │  │  ├── pswd (WMS登录用户) │  │
│   ├─ 新增账套向导(6步)                │       │  │  ├── DICT_TAB/FLD      │  │
│   └─ 初始化系统库                      │       │  │  └── SYS_ACCOUNT_SET ★ │  │
│                                     │       │  └───────────┬───────────┘  │
└──────────────────────────────────────┘       │              │              │
                                                 │  ┌───────────▼───────────┐  │
                                                 │  │  db_gz01 / db_gz02 ... │  │
                                                 │  │  (各账套业务数据库)      │  │
                                                 │  └───────────────────────┘  │
                                                 └─────────────────────────────┘
                                                        ▲               ▲
                        ┌───────────────────────────────┘               │
                        │  API启动时读取账套列表    每次请求按用户选择的账套连接
                        ▼                                               ▼
┌─────────────────────────┐     ┌──────────────────────────────────────────────┐
│   WMS Web 登录页面       │────▶│         WmsPlus.Api (Web API)                │
│                         │     │  GET /api/auth/accounts → 返回所有可用账套列表  │
│  ┌───────────────────┐  │     │  POST /api/auth/login                        │
│  │ 账套下拉选择       │  │     │    { CompNo:"GZ01", Username:"admin", ... }  │
│  │ GZ01-天津渤海... ✓│  │     │    ↓                                         │
│  │ GZ02-天津渤海测试 │  │     │  验证pswd(COMPNO=GZ01,USR=admin)             │
│  └───────────────────┘  │     │  ↓                                           │
│  用户名 [___________]  │     │  用GZ01的DB配置创建 WarehouseDbContext          │
│  密码   [___________]  │     │  JWT中携带CompNo claim                        │
│  [登录]                │     │  后续请求均使用该账套的数据库                   │
└─────────────────────────┘     └──────────────────────────────────────────────┘
```

### 2.1 登录页账套选择流程（参考截图第11张）

```
用户打开WMS登录页
    │
    ▼
前端调用 GET /api/auth/accounts（无需认证）
    │
    ▼
后端查询 wmssystem.SYS_ACCOUNT_SET，返回所有账套:
  [{ CompNo:"GZ01", CompName:"天津渤海化学试剂测试账套" },
   { CompNo:"GZ02", CompName:"天津渤海化学试剂测试账套" }]
    │
    ▼
登录页渲染账套下拉框（参考截图UI）:
  ┌──────────────────────────────────────┐
  │ GZ01-天津渤海化学试剂测试账套     ▾ │  ← 下拉展开显示所有账套
  ├──────────────────────────────────────┤
  │ GZ01-天津渤海化学试剂测试账套         │
  │ GZ02-天津渤海化学试剂测试账套         │
  └──────────────────────────────────────┘
    │
    ▼
用户选择账套 → 输入用户名密码 → 点击登录
    │
    ▼
POST /api/auth/login { CompNo:"GZ01", Username:"admin", Password:"xxx" }
    │
    ▼
后端: WHERE pswd.COMPNO='GZ01' AND pswd.NAME='admin'
验证通过 → 生成JWT(含CompNo=GZ01 claim)
    │
    ▼
后续所有业务请求: 从JWT取CompNo → 连接对应账套数据库 → 返回数据
```

---

## 三、技术选型

| 组件 | 技术方案 | 说明 |
|------|---------|------|
| C/S 客户端 | **WinForms + .NET 8.0** | 轻量桌面应用，参考UI为经典Windows蓝白风格 |
| 数据访问 | **MySqlConnector + Dapper** | 轻量级数据访问，无需EF Core重量级依赖 |
| UI 风格 | 参考截图的经典蓝白配色 | 标题栏"AI-WMS PLUS 智能仓储"，www.amtbts.com |
| 安装方式 | **Inno Setup / NSIS** 打包安装程序 | 安装时执行SQL初始化脚本 |
| 项目位置 | `e:\AICode\WESSystemQD\WmsAccountManager\` | 与现有WmsPlus/WmsPlus.Api同级 |

---

## 四、数据库设计

### 4.1 wmssystem 新增表：SYS_ACCOUNT_SET（账套配置表）

```sql
CREATE TABLE IF NOT EXISTS `SYS_ACCOUNT_SET` (
  `ID` INT AUTO_INCREMENT PRIMARY KEY COMMENT '主键',
  `COMP_NO` VARCHAR(20) NOT NULL COMMENT '公司代号(账套代码，如GZ01)',
  `COMP_NAME` VARCHAR(100) NOT NULL COMMENT '公司名称',
  `DB_NAME` VARCHAR(50) NOT NULL COMMENT '数据库名称(如db_gz01)',
  `DB_SERVER` VARCHAR(100) DEFAULT 'localhost' COMMENT '数据库服务器地址',
  `DB_PORT` INT DEFAULT 3306 COMMENT '数据库端口',
  `DB_USER` VARCHAR(50) DEFAULT 'root' COMMENT '数据库连接用户名',
  `DB_PASSWORD` VARCHAR(100) DEFAULT '' COMMENT '数据库连接密码(AES加密存储)',
  `ADMIN_USR` VARCHAR(30) DEFAULT 'admin' COMMENT '该账套的WMS管理员登录名',
  `ADMIN_PWD` VARCHAR(100) DEFAULT '' COMMENT '该账套的WMS管理员密码(AES加密存储)',
  `IS_ACTIVE` TINYINT(1) DEFAULT 0 COMMENT '是否为默认账套(0否,1是,降级用)',
  `REMARK` VARCHAR(255) DEFAULT '' COMMENT '备注',
  `CREATED_AT` DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `UPDATED_AT` DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  UNIQUE KEY `UK_COMP_NO` (`COMP_NO`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='账套配置表';
```

> **关键字段说明**：
> - `ADMIN_USR` / `ADMIN_PWD`：存储该账套的 **WMS 系统登录管理员** 的用户名和密码
> - 创建账套时，同时将此用户写入 `pswd` 表（COMPNO=账套代号, USR=管理员用户名）
> - WMS Web端登录时，通过 `pswd` 表验证用户名密码
> - `IS_ACTIVE`：降级使用场景（无JWT时的默认账套），正常流程以登录页选择为准

### 4.2 pswd 表与账套的关系（已有表）

```
wmssystem.pswd 表结构（复合主键: COMPNO + USR）:
┌────────┬──────┬──────┬──────────┬──────────┐
│ COMPNO │ USR  │ NAME │ PWD      │ DEP      │
├────────┼──────┼──────┼──────────┼──────────┤
│ GZ01   │ admin│ 管理员│ ****     │          │  ← GZ01账套的管理员
│ GZ01   │ zhang│ 张三  │ ****     │ 仓库部    │  ← GZ01账套的普通用户
│ GZ02   │ admin│ 管理员│ ****     │          │  ← GZ02账套的管理员
│ GZ02   │ lisi │ 李四  │ ****     │ 仓库部    │  ← GZ02账套的普通用户
└────────┴──────┴──────┴──────────┴──────────┘

每个账套(COMPNO) 有自己独立的用户体系！
```

### 4.3 数据关系全景图

```
wmssystem 库 (系统库):
├── pswd              (已有 - WMS登录用户表, 复合主键 COMPNO+USR)
│   ├── COMPNO='GZ01', USR='admin'  ← 由账套管理器创建时自动写入
│   ├── COMPNO='GZ01', USR='zhang'  ← 后续在WMS系统中添加
│   ├── COMPNO='GZ02', USR='admin'  ← 由账套管理器创建时自动写入
│   └── ...
├── DICT_TAB          (已有 - 字典表头)
├── DICT_FLD          (已有 - 字典表身)
└── SYS_ACCOUNT_SET   (新增 - 账套配置表)
    ├── 存储账套连接参数(DB_SERVER, DB_USER, DB_PASSWORD...)
    ├── 存储账套管理员凭据(ADMIN_USR, ADMIN_PWD)
    └── IS_ACTIVE 标记默认账套(降级用)

各账套库 (db_gz01, db_gz02, ...):
├── MF_RKTZ           (入库通知单表头)
├── TF_RKTZ           (入库通知单表身)
├── MY_WH             (仓库主数据)
├── MF_JGZY           (加工资源主档)
... (更多业务表，由初始化SQL脚本批量创建)
```

---

## 五、C/S 程序功能模块设计

### 5.1 主界面（参考截图第2张）

```
┌─────────────────────────────────────────────────────┐
│  SCompCreate                              [─] [□] [×] │
├─────────────────────────────────────────────────────┤
│  ████ AI-WMS PLUS 智能仓储                           │
│  www.mnlts.com                                       │
│                                                      │
│  ┌──────────────────────────────────────────────┐   │
│  │  账套公司列表                    [新增] [删除]  │   │
│  ├────────────┬─────────────────────────────────┤   │
│  │ ▶ GZ01     │ 天津渤海化学试剂有限公司           │   │
│  │   GZ02     │ 天津渤海化学测试账套               │   │
│  └────────────┴─────────────────────────────────┘   │
│                                                      │
│  ┌──────────────────────────────────────────────┐   │
│  │              [高级设置]  [确定]  [取消]         │   │
│  └──────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────┘
```

**功能说明：**
- **列表区**：显示所有已注册账套，树形展示（公司代号为节点），双击可设为默认账套
- **新增按钮**：打开6步新增账套向导
- **删除按钮**：删除选中账套（需确认，不可删除当前激活账套，同时清理对应pswd用户）
- **高级设置**：修改选中账套的所有参数（含数据库连接和管理员密码）
- **确定**：保存当前选择并退出
- **取消**：放弃修改并退出

### 5.2 新增账套向导 — 完整6步（参考全部9张截图）

#### 步骤① 欢迎页（参考截图第3张）
```
┌──────────────────────────────────────────────────┐
│  SCompCreate                            [─][□][×] │
├──────────────────────────────────────────────────┤
│  ████ AI-WMS PLUS 智能仓储                       │
│                                                  │
│       新增账套向导:                               │
│       请点击[下一步]开始新增账套                   │
│                                                  │
│              [上一步(F7)]  [下一步(F8)]  [取消]    │
└──────────────────────────────────────────────────┘
```

#### 步骤② 公司资料输入（参考截图第4张）
```
┌──────────────────────────────────────────────────┐
│  SCCompCreate                                     │
├──────────────────────────────────────────────────┤
│  ████ AI-WMS PLUS 智能仓储                       │
│                                                  │
│       公司资料输入                                │
│                                                  │
│       公司代号  [_______________]                 │
│       公司名称  [_______________]                 │
│       选择数据库 [MySQL        ▾]                 │
│                                                  │
│       [上一步(F7)]  [下一步(F8)]  [取消(ESC)]     │
└──────────────────────────────────────────────────┘
```
| 字段 | 说明 | 校验规则 |
|------|------|---------|
| 公司代号 | 如 GZ01、GZ02 | 必填，唯一，字母数字下划线，≤20字符 |
| 公司名称 | 如天津渤海化学试剂有限公司 | 必填，≤100字符 |
| 选择数据库 | 下拉选择 | 固定 MySQL 选项 |

#### 步骤③ 数据库连接信息（参考截图第5张）
```
┌──────────────────────────────────────────────────┐
│  SCCompDbCreate                                  │
├──────────────────────────────────────────────────┤
│  ████ AI-WMS PLUS 智能仓储                       │
│                                                  │
│       数据库连接信息                              │
│                                                  │
│       服务器名称 [GZTXTS_________]                │
│       用户名    [sa____________]                  │
│       密码      [************]                    │
│                                                  │
│       [上一步(F7)]  [下一步(F8)]  [取消(ESC)]     │
└──────────────────────────────────────────────────┘
```
| 字段 | 说明 | 默认值 | 备注 |
|------|------|--------|------|
| 服务器名称 | MySQL服务器地址或主机名 | localhost | 参考图用GZTXTS作为示例 |
| 用户名 | 数据库连接用户名 | root | MySQL登录用户 |
| 密码 | 数据库连接密码 | （输入） | 用于创建新数据库 |

> **注意**：这是 **数据库服务器的连接凭据**（用于创建新账套数据库），不是WMS登录用户！

#### 步骤④ 数据库参数设置（参考截图第6张）
```
┌──────────────────────────────────────────────────────┐
│  SCompDbCreate                                        │
├──────────────────────────────────────────────────────┤
│  ████ AI-WMS PLUS 智能仓储                             │
│                                                        │
│  新建数据库预设大小  [20      ▲▼]  ☑ 库文件自动增长      │
│                                                        │
│  自动增长        最大限制                               │
│  ○ 按百分比增长 [10 ▲▼]   ○ 没有限制                    │
│  ○ 按字节增长   [1  ▲▼]   ○ 最大限制(MB) [10 ▼]         │
│                                                        │
│       [上一步(F7)]  [完成 F8]  [取消(ESC)]              │
└──────────────────────────────────────────────────────┘
```
| 字段 | 说明 | 默认值 |
|------|------|--------|
| 新建数据库预设大小 | 数据库初始大小(MB) | 20 |
| 库文件自动增长 | 是否启用自动扩展 | 勾选(是) |
| 自动增长方式 | 按百分比 / 按字节 | 按百分比增长 10% |
| 最大限制 | 没有限制 / 设定上限(MB) | 没有限制 |

#### 步骤⑤ 账套管理员设置（参考截图第7张）— **关键步骤**
```
┌────────────────────────────────────────────┐
│  CMPswdInput                           [×] │
├────────────────────────────────────────────┤
│                                            │
│  请输入帐套管理员用户和密码                   │
│                                            │
│  用户名  [admin          ▾]                │
│  密码    [________________]                │
│                                            │
│         [确定(F8)]    [取消(ESC)]           │
└────────────────────────────────────────────┘
```
| 字段 | 说明 | 备注 |
|------|------|------|
| 用户名 | WMS系统管理员登录名 | 下拉可选或手动输入，默认 `admin` |
| 密码 | WMS系统管理员登录密码 | 该账套的首次登录密码 |

> **核心逻辑**：此步骤设置的 **用户名和密码是用于登录WMS Web系统的**，不是数据库密码！
>
> 创建账套时，程序会自动将此用户写入 `wmssystem.pswd` 表：
> ```sql
> INSERT INTO pswd (COMPNO, USR, NAME, PWD) VALUES ('GZ01', 'admin', '管理员', '加密后密码')
> ```

#### 步骤⑥ 执行创建（参考截图第8、9张）

点击"完成"后弹出进度条页面：

```
┌──────────────────────────────────────────────────┐
│  SCompCreate                                     │
├──────────────────────────────────────────────────┤
│  ████ AI-WMS PLUS 智能仓储                       │
│                                                  │
│       请点击完成键，系统自动执行                   │
│                                                  │
│       正在刷新表 [MF_JGZY]...                     │
│       ████████████████████░░░░░░░░░  60%         │
│                                                  │
│       [上一步(F7)]  [完成 F8]  [取消(ESC)]       │
└──────────────────────────────────────────────────┘
```

**执行流程（按顺序）：**
1. 用步骤③的数据库凭据连接 MySQL Server
2. 创建新数据库（名称 = `db_{公司代号小写}`，或自定义）
3. 设置数据库参数（大小、自动增长等）
4. **逐表创建业务表**（进度条实时更新，显示"正在刷新表[表名]..."）
5. 在 `wmssystem.SYS_ACCOUNT_SET` 插入账套配置记录
6. 在 `wmssystem.pswd` 插入管理员用户记录（COMPNO=账套代号）
7. 弹出成功提示：**"创建资料库成功"**

### 5.3 高级设置对话框（编辑已有账套）

从主界面点击"高级设置"按钮弹出，可修改：
- 公司名称
- 数据库连接信息（服务器、用户名、密码）
- 账套管理员用户名/密码（同步更新 pswd 表）
- 是否设为默认账套(IS_ACTIVE)
- 备注

---

## 六、WMS 登录页改造方案（新增）

### 6.1 当前状态
当前 [Login.razor](file:///e:/AICode/WESSystemQD/WmsPlus/Pages/Login.razor) 第46-53行的账套选择器是**硬编码**的：
```html
<div class="account-select">
    <span>GZ01-天津渤海化学试剂测试账套</span>
</div>
```

### 6.2 改造后的登录页账套选择器（参考截图第11张）

```
┌──────────────────────────────────────────┐
│ GZ01-天津渤海化学试剂测试账套     ▾     │  ← 可点击下拉
├──────────────────────────────────────────┤
│ GZ01-天津渤海化学试剂测试账套           │  ← 选项列表
│ GZ02-天津渤海化学试剂测试账套           │
└──────────────────────────────────────────┘
```

**UI交互说明：**
- 默认选中第一个账套或上次登录的账套（从 localStorage 恢复）
- 点击展开下拉列表，显示所有可用账套（公司代号 + 公司名称）
- 选择后收起下拉，显示"代号-名称"格式文本
- 切换账套时清空用户名和密码输入框（不同账套用户体系不同）

### 6.3 前端改动 — Login.razor

**新增字段：**
```csharp
@code {
    private List<AccountDto> accounts = new();
    private string selectedCompNo = "";
    private string selectedAccountName = "";
    private bool showAccountDropdown = false;
}
```

**页面加载时调用新接口获取账套列表：**
```csharp
private async Task LoadAccounts()
{
    var response = await _httpClient.GetFromJsonAsync<List<AccountDto>>(
        $"{ApiBaseUrl}/api/auth/accounts");
    if (response != null && response.Count > 0)
    {
        accounts = response;
        if (string.IsNullOrEmpty(selectedCompNo))
        {
            selectedCompNo = accounts[0].CompNo;
            selectedAccountName = $"{accounts[0].CompNo}-{accounts[0].CompName}";
        }
    }
}
```

**登录请求携带 CompNo：**
```csharp
var result = await AuthService.LoginAsync(username, password, selectedCompNo);
```

**AuthService.LoginAsync 签名变更：**
```csharp
public async Task<LoginResponse> LoginAsync(string username, string password, string compNo)
{
    var response = await _httpClient.PostAsJsonAsync($"{ApiBaseUrl}/api/auth/login", new
    {
        Username = username,
        Password = password,
        CompNo = compNo
    });
    // ...
}
```

**localStorage 新增存储项：** `wms_selected_compno` — 上次选择的账套代号

### 6.4 后端改动 — AuthController

#### 6.4.1 新增接口：获取账套列表（无需认证）
```csharp
[HttpGet("accounts")]
[AllowAnonymous]
public async Task<ActionResult<IEnumerable<AccountDto>>> GetAccounts()
{
    var accounts = await _context.AccountSets
        .OrderBy(a => a.CompNo)
        .Select(a => new AccountDto { CompNo = a.CompNo, CompName = a.CompName })
        .ToListAsync();
    return Ok(accounts);
}
```

#### 6.4.2 改造登录接口

**LoginRequest 增加 CompNo：**
```csharp
public class LoginRequest
{
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public string CompNo { get; set; } = "";      // 新增
}
```

**Login 方法核心改动 — 按 COMPNO+NAME 双条件查用户：**
```csharp
// 校验 CompNo 必填
if (string.IsNullOrWhiteSpace(request.CompNo))
    return BadRequest("请选择账套");

// 关键改动：增加 COMPNO 条件
var user = await _context.Users
    .FirstOrDefaultAsync(u => u.COMPNO == request.CompNo && u.NAME == request.Username);
```

> **关键变化对比**：
> | 项目 | 改造前 | 改造后 |
> |------|--------|--------|
> | 用户查询 | `WHERE NAME = @Username` | `WHERE COMPNO = @CompNo AND NAME = @Username` |
> | 账套来源 | 无（全局唯一） | 前端传入 + JWT携带 |
> | 数据库连接 | 启动时固定一个 | 每次请求按JWT中CompNo动态决定 |

### 6.5 CSS 样式改动 — login.css

在现有 `.account-select` 样式基础上增加下拉交互样式：
- `.account-dropdown` — 下拉选项容器（绝对定位、阴影、最大高度200px可滚动）
- `.account-dropdown-item` — 单个选项（padding、hover高亮、选中态蓝色背景）

---

## 七、WMS API 改造方案（WarehouseDbContext 动态化）

### 7.1 核心改造：WarehouseDbContext 会话级动态化

> **架构决策**：不再使用全局唯一活跃账套，改为 **每个用户登录时选择账套，JWT 携带 CompNo，每次请求根据 JWT 中的 CompNo 动态连接对应账套数据库**。多个用户可同时使用不同账套。

**改造后（基于JWT CompNo的动态工厂）：**

```csharp
builder.Services.AddScoped<WarehouseDbContext>(provider =>
{
    var httpContext = provider.GetService<IHttpContextAccessor>()?.HttpContext;
    string compNo = null;

    // 1. 从JWT提取CompNo
    if (httpContext?.User != null)
        compNo = httpContext.User.FindFirst("CompNo")?.Value;

    // 2. 降级：无JWT时读取IS_ACTIVE默认账套
    if (string.IsNullOrEmpty(compNo))
    {
        using var sysDb = new AppDbContext(sysOptions);
        var activeAccount = sysDb.AccountSets.FirstOrDefault(a => a.IsActive);
        if (activeAccount != null) compNo = activeAccount.CompNo;
    }

    // 3. 用CompNo查找数据库连接信息（优先从缓存读）
    var account = accountCache.GetAccount(compNo);

    // 4. 解密并构建 WarehouseDbContext 连接
    var warehouseConnStr = $"Server={account.DbServer};Database={account.DbName};...";
    return new WarehouseDbContext(whOptions);
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IAccountCache, AccountCache>();
```

### 7.2 新增实体类

```csharp
[Table("SYS_ACCOUNT_SET")]
public class SysAccountSet
{
    public int Id { get; set; }
    public string CompNo { get; set; }
    public string CompName { get; set; }
    public string DbName { get; set; }
    public string DbServer { get; set; }
    public int DbPort { get; set; }
    public string DbUser { get; set; }
    public string DbPassword { get; set; }     // AES加密
    public string AdminUsr { get; set; }
    public string AdminPwd { get; set; }       // AES加密
    public bool IsActive { get; set; }         // 降级用
    public string Remark { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class AccountDto
{
    public string CompNo { get; set; } = "";
    public string CompName { get; set; } = "";
}
```

### 7.3 AppDbContext 增加 DbSet

```csharp
public DbSet<SysAccountSet> AccountSets { get; set; }
```

---

## 八、项目文件结构

```
e:\AICode\WESSystemQD\
├── WmsPlus/                    # 已有 - Blazor前端（需改造登录页）
│   ├── Pages/Login.razor        # ← 改造：账套选择器改为动态下拉
│   ├── Services/AuthService.cs  # ← 改造：LoginAsync增加CompNo参数
│   └── wwwroot/css/login.css    # ← 改造：增加账套下拉框样式
├── WmsPlus.Api/                # 已有 - Web API后端（需改造）
│   ├── Controllers/AuthController.cs  # ← 改造：新增accounts接口+login改CompNo查询
│   ├── Data/AppDbContext.cs     # ← 改造：增加AccountSets DbSet
│   ├── Data/SysAccountSet.cs    # ← 新增：账套实体
│   ├── Models/User.cs           # ← 改造：LoginRequest增加CompNo
│   └── Program.cs               # ← 改造：WarehouseDbContext动态工厂模式
├── WmsAccountManager/          # 新增 - 账套管理器(C/S)
│   ├── WmsAccountManager.sln
│   └── WmsAccountManager/
│       ├── Program.cs              # WinForms入口
│       ├── MainForm.cs             # 主界面（账套列表）
│       ├── WizardForm.cs           # 新增账套向导(6步)
│       ├── AdminUserForm.cs        # 步骤⑤: 账套管理员对话框
│       ├── SetConfigForm.cs        # 高级设置对话框
│       ├── DbHelper.cs             # MySQL连接与CRUD封装
│       ├── CryptoHelper.cs         # AES加解密工具
│       ├── Models/AccountSet.cs    # 账套模型
│       └── SQL/
│           ├── init_wmssystem.sql       # wmssystem建库建表脚本
│           └── init_accountset_tables.sql  # 账套库业务表创建脚本
└── plans/
```

---

## 九、开发步骤（按顺序执行，共30步）

### 阶段一：数据库基础
1. 编写 `init_wmssystem.sql` — wmssystem 完整建表脚本（pswd/DICT_TAB/DICT_FLD + SYS_ACCOUNT_SET）
2. 编写 `init_accountset_tables.sql` — 从 db_gz01 导出所有业务表 CREATE TABLE 作为模板
3. 在 MySQL 中验证 SYS_ACCOUNT_SET 表创建成功

### 阶段二：WMS 后端改造（API层）
4. 新增 `SysAccountSet.cs` + `AccountDto.cs` 实体类
5. `AppDbContext.cs` 添加 `DbSet<SysAccountSet>` 及映射
6. `User.cs` 的 LoginRequest 增加 CompNo 属性
7. `AuthController.cs` 新增 `[HttpGet("accounts")]` 接口（AllowAnonymous）
8. `AuthController.Login()` 改为 COMPNO+NAME 双条件查询
9. `Program.cs` 注册 IHttpContextAccessor，WarehouseDbContext 改为基于JWT CompNo的动态工厂
10. 实现 IAccountCache 缓存服务
11. 测试API：选账套登录成功、JWT携带正确CompNo

### 阶段三：WMS 前端改造（登录页）— **新增**
12. **Login.razor** — 硬编码选择器改为动态下拉框（加载账套列表、localStorage恢复、切换清空输入）
13. **AuthService.cs** — LoginAsync 增加 compNo 参数
14. **login.css** — 增加下拉框样式
15. 前端联调：登录页显示账套列表、切换账套、带CompNo登录成功

### 阶段四：C/S 账套管理器开发
16. 创建 WmsAccountManager WinForms 项目（.NET 8.0）
17. CryptoHelper.cs — AES加解密（与API共用密钥）
18. DbHelper.cs — MySQL连接与CRUD封装
19. MainForm.cs — 主界面（账套树形列表、蓝白UI）
20. WizardForm.cs — 6步向导框架
21. 向导步骤②：公司资料输入页
22. 向导步骤③：数据库连接信息页（含测试连接）
23. 向导步骤④：数据库参数设置页
24. AdminUserForm.cs — 步骤⑤：账套管理员对话框
25. 向导步骤⑥：执行创建页（进度条逐表创建）
26. SetConfigForm.cs — 高级设置对话框
27. 首次启动检测与 wmssystem 初始化引导
28. UI美化（蓝白配色）

### 阶段五：全链路联调
29. C/S创建GZ02账套 → WMS登录页选择GZ02 → admin登录 → 操作数据来自db_gz02
30. 切回GZ01登录 → 数据来自db_gz01

---

## 十、关键技术点说明

### 10.1 两层密码的区别（非常重要）

| 密码类型 | 存储位置 | 用途 | 设置时机 |
|----------|---------|------|---------|
| **数据库密码** | SYS_ACCOUNT_SET.DB_PASSWORD | 连接MySQL服务器创建/访问账套库 | 向导步骤③ |
| **WMS登录密码** | SYS_ACCOUNT_SET.ADMIN_PWD + pswd.PWD | 用户登录WMS Web系统 | 向导步骤⑤ |

两者完全独立，分别AES加密存储。

### 10.2 架构决策：会话级 vs 全局账套

| 方案 | 说明 | 优劣 |
|------|------|------|
| **全局活跃账套**(IS_ACTIVE) | C/S管理器设定唯一活跃账套，API全局使用 | 简单，但同一时刻只能用一个账套 |
| **会话级账套**(JWT CompNo) ✅ | 用户登录时自选账套，JWT携带，每次请求动态连接 | 灵活，多用户可同时用不同账套 |

**本方案采用会话级为主 + IS_ACTIVE降级为辅的双重策略。**

### 10.3 AES 加解密策略
- 算法：AES-256-CBC
- 密钥：C/S 和 API 共享硬编码常量
- 加密范围：DB_PASSWORD、ADMIN_PWD、pswd.PWD

### 10.4 账套库SQL初始化
- 从 db_gz01 导出完整 DDL
- 向导步骤⑥逐表执行，进度条实时反馈 "正在刷新表[{表名}]..."
- 完成后弹窗 "创建资料库成功"

### 10.5 性能优化
- IAccountCache Singleton 缓存账套配置
- WarehouseDbContext Scope 级别创建
- 提供 refresh 接口供账套变更后刷新缓存

---

## 十一、风险与注意事项

| 风险点 | 应对措施 |
|--------|---------|
| API启动时无任何账套 | 明确错误提示："请先在账套管理器中创建账套" |
| 账套数据库连接失败 | 返回详细错误信息（服务器名、数据库名、MySQL错误码） |
| 并发操作冲突 | 事务 + 行锁；缓存设短TTL |
| 账套库表结构升级 | SYS_ACCOUNT_SET 增加SCHEMA_VERSION字段 |
| pswd写入失败导致无法登录 | 事务性写入（SYS_ACCOUNT_SET + pswd 全成功或全回滚） |
| 删除账套残留用户 | 级联删除 pswd 中对应 COMPNO 的所有用户记录 |
