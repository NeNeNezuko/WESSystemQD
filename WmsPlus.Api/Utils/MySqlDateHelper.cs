using System.Reflection;

namespace WmsPlus.Api.Utils;

/// <summary>
/// MySQL 日期类型安全转换工具
/// 解决 MySqlConnector.MySqlDateTime 无法被 System.Text.Json 序列化的问题
/// </summary>
public static class MySqlDateHelper
{
    /// <summary>
    /// 安全转换实体对象中的所有 DateTime/DateTime? 属性
    /// 将 MySqlConnector.MySqlDateTime 类型转换为 System.DateTime
    /// 使用反射拷贝所有属性，对 DateTime 类型属性做特殊处理
    /// </summary>
    public static T SafeConvert<T>(T source) where T : class, new()
    {
        if (source == null)
            return null!;

        var result = new T();
        var sourceType = typeof(T);
        var properties = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var prop in properties)
        {
            if (!prop.CanRead || !prop.CanWrite)
                continue;

            var value = prop.GetValue(source);
            var propType = prop.PropertyType;

            // 处理 DateTime? (Nullable<DateTime>)
            if (IsNullableDateTime(propType))
            {
                prop.SetValue(result, SafeToDateTime(value));
                continue;
            }

            // 处理 DateTime（非可空）
            if (propType == typeof(DateTime))
            {
                var dt = SafeToDateTime(value);
                prop.SetValue(result, dt ?? default);
                continue;
            }

            // 其他类型直接复制
            prop.SetValue(result, value);
        }

        return result;
    }

    private static bool IsNullableDateTime(Type type)
    {
        return type.IsGenericType &&
               type.GetGenericTypeDefinition() == typeof(Nullable<>) &&
               type.GetGenericArguments()[0] == typeof(DateTime);
    }

    /// <summary>
    /// 安全将任意值转换为 DateTime?（处理 MySqlDateTime 类型）
    /// </summary>
    public static DateTime? SafeToDateTime(object? value)
    {
        if (value == null)
            return null;

        if (value is DateTime dt)
            return dt;

        try
        {
            var type = value.GetType();

            // 处理 MySqlConnector.MySqlDateTime
            if (type.Name == "MySqlDateTime" ||
                type.FullName?.Contains("MySqlDateTime") == true)
            {
                // 尝试调用 GetDateTime() 方法
                var getDtMethod = type.GetMethod("GetDateTime", Type.EmptyTypes);
                if (getDtMethod != null)
                    return (DateTime)getDtMethod.Invoke(value, null)!;

                // 尝试读取 Value 属性
                var valueProp = type.GetProperty("Value");
                if (valueProp != null)
                    return (DateTime)valueProp.GetValue(value)!;

                // 尝试通过 Convert 转换
                return Convert.ToDateTime(value);
            }

            return Convert.ToDateTime(value);
        }
        catch
        {
            return null;
        }
    }
}
