# 项目规则

## Git 仓库配置

本项目使用以下 Git 仓库进行代码管理：

- **仓库地址**: https://github.com/NeNeNezuko/WESSystemQD.git
- **默认分支**: main
- **用户名**: NeNeNezuko
- **邮箱**: 985604681@qq.com

### 提交代码时的注意事项

1. 提交前确保代码已通过 lint 和 typecheck 检查
2. 使用有意义的提交信息
3. 不要提交敏感信息（如 appsettings.Development.json 中的密码等）

## 项目结构

- `WmsPlus/` - Blazor WebAssembly 前端项目
- `WmsPlus.Api/` - ASP.NET Core API 后端项目

## 数据库配置

本项目使用 MySQL 数据库，包含两个数据库实例：

### 数据库列表

| 连接名称 | 数据库名 | 用途 | 对应 DbContext |
|---------|---------|------|---------------|
| `DefaultConnection` | `wmssystem` | 管理账户数据（用户、权限等） | `AppDbContext` |
| `WarehouseConnection` | `db_gz01` | 管理仓库信息（库存、出入库等） | `WarehouseDbContext` |

### 数据库连接信息

**数据库类型**: MySQL  
**服务器地址**: localhost  
**端口**: 3306（默认）  
**用户名**: root  
**字符集**: utf8mb4  

### 配置文件位置

数据库连接字符串配置在 `WmsPlus.Api/appsettings.json` 文件中：

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=wmssystem;Uid=root;Pwd=xxx;Charset=utf8mb4;",
    "WarehouseConnection": "Server=localhost;Database=db_gz01;Uid=root;Pwd=xxx;Charset=utf8mb4;"
  }
}
```

### 数据库上下文类

| 类名 | 文件路径 | 对应数据库 |
|------|---------|-----------|
| `AppDbContext` | `WmsPlus.Api/Data/AppDbContext.cs` | `wmssystem` |
| `WarehouseDbContext` | `WmsPlus.Api/Data/WarehouseDbContext.cs` | `db_gz01` |

### 注意事项

1. **敏感信息保护**: 生产环境中密码不应硬编码在配置文件中，应使用环境变量或 .NET Secret Manager
2. **数据库初始化**: 确保两个数据库实例已在 MySQL 中创建
3. **连接池**: 使用默认连接池配置，可根据需要调整连接池大小