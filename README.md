# DVersionTool

简单搞了一个来满足自己工作上的需要，先用着，下面一个版本在进行一些重构来完善代码。体验下不管不顾的写代码。

## 命令

可用|命令|可选参数|示例|说明
:-:|:-:|:-|:-|:-
是|config|-r [configName]|dvt config -r|创建或者刷新配置文件，默认配置文件名 `dvt.config.josn`
否||-ig projectName|dvt config -ig test.csproj|忽略对 `test.csproj` 项目版本号的管理
||||
是|add||dvt add|对管理中的项目，`build` 版本号 +1
否||-ma|dvt add -ma|对管理中的项目，`major` 版本号 +1；并且将下级版本号置 0
否||-mi|dvt add -mi|对管理中的项目，`minor` 版本号 +1；并且将下级版本号置 0
否||-r|dvt add -r|对管理中的项目，`revision` 版本号 +1；并且将下级版本号置 0
否||-b|dvt add -b|对管理中的项目，`build` 版本号 +1
||||
是|set||dvt set|将处于管理中的项目的版本号置为配置文件中的 `CurrVersion` 对应的版本号
是|set|-v|dvt set -v 10.2.0.4|将项目版本号设置为 `10.2.0.4`
