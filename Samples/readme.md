# Senparc.Weixin SDK Demo

## 项目文件夹说明

| 文件夹 | 说明 |
|--------|--------|
|   net6-mvc      |   【推荐】ASP.NET 6.0 示例，可用于直接部署
|   netcore3.1-mvc      |   【即将停止更新】ASP.NET Core 3.1 示例，可用于直接部署
|   console             |   命令行注册过程演示 Demo（接口调可参考 Web 项目）
|   net45-mvc           |   【停止更新】ASP.NET 4.5 MVC 示例，可用于直接部署，此项目中包含了 CommonServices 项目，供其他各 Sample 公用
|   net45-webforms      |   【停止更新】ASP.NET 4.5 Web Forms 示例，主要演示和 MVC 项目有差异的部分，详细演示请见 MVC 项目
|   Senparc.Weixin.Sample.CommonService      |   所有 Sample 中共享的公共代码库（仅为 Sample 服务，和 SDK 源码无关）


## 解决方案文件（sln）说明

> 解决方案文件（.sln）如有写明 Visual Studio 版本，如：`Senparc.Weixin.MP.Sample.vs2017.sln`，则表明此项目需要使用 Visual Studio 2017 或以上打开。

## 帮你选择

> 如果你希望学习并使用最新的 .NET 6.0（preview）框架，并且已经安装了 VS2019（v16.9 以上），并且希望调试 .NET 6.0 及以上版本，那么请打开：net6-mvc/Senparc.Weixin.Sample.Net6.sln 解决方案

> 如果你已经安装了 VS2019（v16.3以上），并且希望调试 .NET Core 3.0 及以上版本，那么请打开：netcore3.1-mvc/Senparc.Weixin.Sample.NetCore3.vs2019.sln 解决方案

> 如果你希望将 Senparc.Weixin SDK 用于命令行或桌面应用，那么请打开：console/Senparc.Weixin.MP.Sample.Consoles.vs2019.sln 解决方案

> 其他情况（如没有安装 VS2017，或者只是想调试 .NET Framework 4.5 项目），那么请打开：net45-mvc/Senparc.Weixin.MP.Sample.sln 解决方案

无论选择哪个解决方案，类库的功能都是一致的。
