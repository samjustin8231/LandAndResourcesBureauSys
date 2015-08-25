if exists (select * from sysobjects where id = OBJECT_ID('[sysdiagrams]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [sysdiagrams]

CREATE TABLE [sysdiagrams] (
[name] [nvarchar]  (128) NOT NULL,
[principal_id] [int]  NOT NULL,
[diagram_id] [int]  IDENTITY (1, 1)  NOT NULL,
[version] [int]  NULL,
[definition] [varbinary]  (MAX) NULL)

ALTER TABLE [sysdiagrams] WITH NOCHECK ADD  CONSTRAINT [PK_sysdiagrams] PRIMARY KEY  NONCLUSTERED ( [name],[principal_id],[diagram_id] )
SET IDENTITY_INSERT [sysdiagrams] ON


SET IDENTITY_INSERT [sysdiagrams] OFF
if exists (select * from sysobjects where id = OBJECT_ID('[T_AdministratorArea]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [T_AdministratorArea]

CREATE TABLE [T_AdministratorArea] (
[id] [int]  IDENTITY (1, 1)  NOT NULL,
[name] [varchar]  (50) NOT NULL,
[sort] [int]  NOT NULL,
[pingyin] [varchar]  (80) NOT NULL,
[initial] [varchar]  (30) NOT NULL,
[des] [varchar]  (200) NULL)

SET IDENTITY_INSERT [T_AdministratorArea] ON

INSERT [T_AdministratorArea] ([id],[name],[sort],[pingyin],[initial]) VALUES ( 1,N'京口区',8,N'JingKouQu',N'JKQ')
INSERT [T_AdministratorArea] ([id],[name],[sort],[pingyin],[initial]) VALUES ( 2,N'丹阳区',1,N'DanYangQu',N'DYQ')
INSERT [T_AdministratorArea] ([id],[name],[sort],[pingyin],[initial]) VALUES ( 3,N'句容区',1,N'JuRongQu',N'JRQ')
INSERT [T_AdministratorArea] ([id],[name],[sort],[pingyin],[initial]) VALUES ( 4,N'丹徒区',1,N'DanTuQu',N'DTQ')
INSERT [T_AdministratorArea] ([id],[name],[sort],[pingyin],[initial]) VALUES ( 5,N'扬中区',1,N'YangZhongQu',N'YZQ')
INSERT [T_AdministratorArea] ([id],[name],[sort],[pingyin],[initial]) VALUES ( 6,N'润州区',1,N'RunZhouQu',N'RZQ')
INSERT [T_AdministratorArea] ([id],[name],[sort],[pingyin],[initial]) VALUES ( 7,N'新区',2,N'XinQu',N'XQ')

SET IDENTITY_INSERT [T_AdministratorArea] OFF
if exists (select * from sysobjects where id = OBJECT_ID('[T_ArableAdjust]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [T_ArableAdjust]

CREATE TABLE [T_ArableAdjust] (
[id] [int]  IDENTITY (1, 1)  NOT NULL,
[batchId] [int]  NOT NULL DEFAULT (0),
[dsBlanceIdFrom] [int]  NOT NULL DEFAULT (0),
[dsBlanceIdTo] [int]  NOT NULL DEFAULT (0),
[area] [decimal]  (15,2) NOT NULL DEFAULT (0),
[des] [varchar]  (100) NULL,
[create_time] [datetime]  NULL,
[is_able] [int]  NOT NULL DEFAULT (1))

ALTER TABLE [T_ArableAdjust] WITH NOCHECK ADD  CONSTRAINT [PK_T_ArableAdjust] PRIMARY KEY  NONCLUSTERED ( [id] )
SET IDENTITY_INSERT [T_ArableAdjust] ON


SET IDENTITY_INSERT [T_ArableAdjust] OFF
if exists (select * from sysobjects where id = OBJECT_ID('[T_Batch]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [T_Batch]

CREATE TABLE [T_Batch] (
[id] [int]  IDENTITY (1, 1)  NOT NULL,
[name] [varchar]  (50) NULL,
[subName] [varchar]  (50) NULL,
[year] [varchar]  (50) NULL,
[administratorArea] [varchar]  (50) NULL,
[approvalAuthority] [varchar]  (50) NULL,
[approvalNo] [varchar]  (50) NULL,
[approvalTime] [date]  NOT NULL,
[batchTypeId] [int]  NOT NULL,
[totalArea] [numeric]  (18,4) NULL,
[addConsArea] [numeric]  (18,4) NULL,
[consArea] [numeric]  (18,4) NULL,
[agriArea] [numeric]  (18,4) NULL,
[arabArea] [numeric]  (18,4) NULL,
[unusedArea] [numeric]  (18,4) NULL,
[hasLevyArea] [numeric]  (18,4) NULL,
[userId] [int]  NOT NULL,
[createTime] [datetime]  NOT NULL,
[isSubmited] [int]  NOT NULL DEFAULT (0),
[isDeleted] [int]  NOT NULL DEFAULT (0),
[des] [text]  NULL)

SET IDENTITY_INSERT [T_Batch] ON

INSERT [T_Batch] ([id],[name],[year],[administratorArea],[approvalAuthority],[approvalNo],[approvalTime],[batchTypeId],[totalArea],[addConsArea],[consArea],[agriArea],[arabArea],[unusedArea],[hasLevyArea],[userId],[createTime],[isSubmited],[isDeleted]) VALUES ( 6,N'城镇批次1',N'2014',N'1',N'城镇批次',N'城镇批次123',N'2015/7/26 0:00:00',1,200.0000,100.0000,100.0000,50.0000,20.0000,50.0000,0.0000,1,N'2015/7/26 16:02:10',0,0)
INSERT [T_Batch] ([id],[name],[year],[administratorArea],[approvalAuthority],[approvalNo],[approvalTime],[batchTypeId],[totalArea],[addConsArea],[consArea],[agriArea],[arabArea],[unusedArea],[hasLevyArea],[userId],[createTime],[isSubmited],[isDeleted]) VALUES ( 3,N'独立选址批次1',N'2014',N'1',N'独立选址批次',N'独立选址批次1',N'2015/7/22 0:00:00',3,500.0000,300.0000,200.0000,200.0000,100.0000,100.0000,0.0000,1,N'2015/7/22 15:04:32',0,0)
INSERT [T_Batch] ([id],[name],[year],[administratorArea],[approvalAuthority],[approvalNo],[approvalTime],[batchTypeId],[totalArea],[addConsArea],[consArea],[agriArea],[arabArea],[unusedArea],[hasLevyArea],[userId],[createTime],[isSubmited],[isDeleted]) VALUES ( 4,N'增减挂钩批次1',N'2014',N'1',N'增减挂钩批次',N'增减挂钩批次1',N'2015/7/25 0:00:00',4,500.0000,300.0000,200.0000,200.0000,100.0000,100.0000,0.0000,1,N'2015/7/25 11:12:30',0,0)

SET IDENTITY_INSERT [T_Batch] OFF
if exists (select * from sysobjects where id = OBJECT_ID('[T_Batch_Demand_Supply_Balance]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [T_Batch_Demand_Supply_Balance]

CREATE TABLE [T_Batch_Demand_Supply_Balance] (
[id] [int]  IDENTITY (1, 1)  NOT NULL,
[batchId] [int]  NOT NULL,
[dsBalanceId] [int]  NOT NULL,
[consArea] [numeric]  (18,4) NULL,
[agriArea] [numeric]  (18,4) NULL,
[arabArea] [numeric]  (18,4) NULL,
[createTime] [datetime]  NOT NULL,
[isSubmited] [int]  NOT NULL DEFAULT (0),
[isDeleted] [int]  NOT NULL DEFAULT (0))

SET IDENTITY_INSERT [T_Batch_Demand_Supply_Balance] ON

INSERT [T_Batch_Demand_Supply_Balance] ([id],[batchId],[dsBalanceId],[consArea],[agriArea],[arabArea],[createTime],[isSubmited],[isDeleted]) VALUES ( 16,4,3,0.0000,80.0000,10.0000,N'2015/7/26 19:02:09',0,0)
INSERT [T_Batch_Demand_Supply_Balance] ([id],[batchId],[dsBalanceId],[consArea],[agriArea],[arabArea],[createTime],[isSubmited],[isDeleted]) VALUES ( 8,3,5,0.0000,0.0000,90.0000,N'2015/7/26 16:12:30',0,0)
INSERT [T_Batch_Demand_Supply_Balance] ([id],[batchId],[dsBalanceId],[consArea],[agriArea],[arabArea],[createTime],[isSubmited],[isDeleted]) VALUES ( 20,4,3,0.0000,0.0000,10.0000,N'2015/7/26 19:21:06',0,0)

SET IDENTITY_INSERT [T_Batch_Demand_Supply_Balance] OFF
if exists (select * from sysobjects where id = OBJECT_ID('[T_Batch_Plan]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [T_Batch_Plan]

CREATE TABLE [T_Batch_Plan] (
[id] [int]  IDENTITY (1, 1)  NOT NULL,
[batchId] [int]  NOT NULL,
[planId] [int]  NOT NULL,
[consArea] [numeric]  (18,4) NULL,
[agriArea] [numeric]  (18,4) NULL,
[arabArea] [numeric]  (18,4) NULL,
[issuedQuota] [numeric]  (18,4) NULL,
[createTime] [datetime]  NOT NULL,
[isSubmited] [int]  NOT NULL DEFAULT (0),
[isDeleted] [int]  NOT NULL DEFAULT (0))

SET IDENTITY_INSERT [T_Batch_Plan] ON

INSERT [T_Batch_Plan] ([id],[batchId],[planId],[consArea],[agriArea],[arabArea],[issuedQuota],[createTime],[isSubmited],[isDeleted]) VALUES ( 10,3,17,50.0000,50.0000,50.0000,0.0000,N'2015/7/26 16:20:16',0,0)
INSERT [T_Batch_Plan] ([id],[batchId],[planId],[consArea],[agriArea],[arabArea],[issuedQuota],[createTime],[isSubmited],[isDeleted]) VALUES ( 11,4,18,0.0000,0.0000,0.0000,50.0000,N'2015/7/26 16:42:00',0,0)
INSERT [T_Batch_Plan] ([id],[batchId],[planId],[consArea],[agriArea],[arabArea],[issuedQuota],[createTime],[isSubmited],[isDeleted]) VALUES ( 22,6,20,10.0000,10.0000,10.0000,1.0000,N'2015/8/24 14:25:11',0,0)

SET IDENTITY_INSERT [T_Batch_Plan] OFF
if exists (select * from sysobjects where id = OBJECT_ID('[T_BatchType]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [T_BatchType]

CREATE TABLE [T_BatchType] (
[id] [int]  IDENTITY (1, 1)  NOT NULL,
[name] [varchar]  (50) NOT NULL,
[des] [varchar]  (200) NULL)

SET IDENTITY_INSERT [T_BatchType] ON

INSERT [T_BatchType] ([id],[name]) VALUES ( 1,N'城镇批次')
INSERT [T_BatchType] ([id],[name]) VALUES ( 2,N'中心城区批次')
INSERT [T_BatchType] ([id],[name]) VALUES ( 3,N'独立选址批次')
INSERT [T_BatchType] ([id],[name]) VALUES ( 4,N'增减挂钩批次')

SET IDENTITY_INSERT [T_BatchType] OFF
if exists (select * from sysobjects where id = OBJECT_ID('[T_DemandSupplyBalance]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [T_DemandSupplyBalance]

CREATE TABLE [T_DemandSupplyBalance] (
[id] [int]  IDENTITY (1, 1)  NOT NULL,
[no] [varchar]  (50) NULL,
[name] [varchar]  (50) NULL,
[year] [varchar]  (50) NULL,
[administratorArea] [varchar]  (50) NULL,
[typeId] [int]  NOT NULL,
[acceptUnit] [varchar]  (50) NULL,
[acceptNo] [varchar]  (50) NULL,
[acceptTime] [datetime]  NULL,
[position] [varchar]  (50) NULL,
[scale] [numeric]  (18,4) NULL,
[agriArea] [numeric]  (18,4) NULL,
[arabArea] [numeric]  (18,4) NULL,
[adjustArea] [numeric]  (18,4) NULL,
[occupyArea] [numeric]  (18,4) NULL,
[remainArea] [numeric]  (18,4) NULL,
[userId] [int]  NOT NULL,
[createTime] [datetime]  NOT NULL,
[isSubmited] [int]  NOT NULL DEFAULT (0),
[isDeleted] [int]  NOT NULL DEFAULT (0),
[batchId] [int]  NOT NULL DEFAULT (0),
[des] [text]  NULL)

SET IDENTITY_INSERT [T_DemandSupplyBalance] ON

INSERT [T_DemandSupplyBalance] ([id],[no],[name],[year],[administratorArea],[typeId],[acceptUnit],[acceptNo],[acceptTime],[position],[scale],[agriArea],[arabArea],[occupyArea],[userId],[createTime],[isSubmited],[isDeleted],[batchId]) VALUES ( 4,N'333',N'土地复垦1',N'2014',N'1',1,N'100',N'100',N'2015/7/25 0:00:00',N'100',200.0000,100.0000,100.0000,100.0000,1,N'2015/7/25 10:22:18',1,0,0)
INSERT [T_DemandSupplyBalance] ([id],[no],[name],[year],[administratorArea],[typeId],[acceptUnit],[acceptNo],[acceptTime],[position],[scale],[agriArea],[arabArea],[adjustArea],[occupyArea],[userId],[createTime],[isSubmited],[isDeleted],[batchId],[des]) VALUES ( 3,N'222',N'增减挂钩拆旧1',N'2014',N'1',3,N'123',N'123',N'2015/7/25 0:00:00',N'123',100.0000,80.0000,80.0000,50.0000,123.0000,1,N'2015/7/25 10:21:16',1,0,0,N'xxx')
INSERT [T_DemandSupplyBalance] ([id],[no],[name],[year],[administratorArea],[typeId],[acceptUnit],[acceptNo],[acceptTime],[position],[scale],[agriArea],[arabArea],[occupyArea],[userId],[createTime],[isSubmited],[isDeleted],[batchId]) VALUES ( 5,N'111',N'异地购买1',N'2014',N'1',2,N'异地购买',N'异地购买1',N'2015/7/25 0:00:00',N'学习学习',400.0000,300.0000,100.0000,100.0000,1,N'2015/7/25 10:20:47',1,0,0)
INSERT [T_DemandSupplyBalance] ([id],[no],[name],[year],[administratorArea],[typeId],[agriArea],[arabArea],[userId],[createTime],[isSubmited],[isDeleted],[batchId],[des]) VALUES ( 12,N'222 【转为复垦】',N'增减挂钩拆旧1 【转为复垦】',N'2014',N'1',1,0.0000,50.0000,0,N'2015/7/26 19:21:06',1,0,4,N'222耕地50.0000 【转为复垦】')
INSERT [T_DemandSupplyBalance] ([id],[no],[name],[year],[administratorArea],[typeId],[acceptUnit],[acceptNo],[acceptTime],[position],[scale],[agriArea],[arabArea],[occupyArea],[userId],[createTime],[isSubmited],[isDeleted],[batchId]) VALUES ( 13,N'异地购买100',N'异地购买100',N'2014',N'1',2,N'02',N'02',N'2015/8/20 0:00:00',N'10',1000.0000,800.0000,500.0000,200.0000,1,N'2015/8/8 21:08:08',1,0,0)

SET IDENTITY_INSERT [T_DemandSupplyBalance] OFF
if exists (select * from sysobjects where id = OBJECT_ID('[T_DemandSupplyBalanceType]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [T_DemandSupplyBalanceType]

CREATE TABLE [T_DemandSupplyBalanceType] (
[id] [int]  IDENTITY (1, 1)  NOT NULL,
[name] [varchar]  (50) NOT NULL,
[des] [varchar]  (200) NULL)

SET IDENTITY_INSERT [T_DemandSupplyBalanceType] ON

INSERT [T_DemandSupplyBalanceType] ([id],[name]) VALUES ( 1,N'土地复垦')
INSERT [T_DemandSupplyBalanceType] ([id],[name]) VALUES ( 2,N'异地购买')
INSERT [T_DemandSupplyBalanceType] ([id],[name]) VALUES ( 3,N'增减挂钩拆旧')

SET IDENTITY_INSERT [T_DemandSupplyBalanceType] OFF
if exists (select * from sysobjects where id = OBJECT_ID('[T_FarmlandAdjustment]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [T_FarmlandAdjustment]

CREATE TABLE [T_FarmlandAdjustment] (
[id] [int]  IDENTITY (1, 1)  NOT NULL,
[year] [varchar]  (10) NOT NULL,
[administratorArea] [varchar]  (20) NULL,
[verifId] [int]  NOT NULL,
[adjustmentReason] [varchar]  (200) NULL,
[approvalArticleNo] [varchar]  (200) NULL,
[approvalTime] [date]  NOT NULL,
[startFarmlandArea] [numeric]  (15,4) NULL DEFAULT (0),
[endFarmlandArea] [numeric]  (15,4) NULL DEFAULT (0),
[startFarmlandArableLandArea] [numeric]  (15,4) NULL DEFAULT (0),
[endFarmlandArableLandArea] [numeric]  (15,4) NULL DEFAULT (0),
[taskArea] [numeric]  (15,4) NULL DEFAULT (0),
[adjustBeforeArea] [numeric]  (15,4) NULL DEFAULT (0),
[adjustBeforeArableArea] [numeric]  (15,4) NULL DEFAULT (0),
[adjustAfterArea] [numeric]  (15,4) NULL DEFAULT (0),
[adjustAfterArableArea] [numeric]  (15,4) NULL DEFAULT (0),
[adjustInArea] [numeric]  (15,4) NULL DEFAULT (0),
[adjustInArableArea] [numeric]  (15,4) NULL DEFAULT (0),
[adjustOutArea] [numeric]  (15,4) NULL DEFAULT (0),
[adjustOutArableArea] [numeric]  (15,4) NULL DEFAULT (0),
[isYearStart] [int]  NULL,
[userId] [int]  NOT NULL,
[createTime] [datetime]  NOT NULL,
[isSubmited] [int]  NOT NULL DEFAULT (0),
[isDeleted] [int]  NOT NULL DEFAULT (0),
[des] [text]  NULL)

SET IDENTITY_INSERT [T_FarmlandAdjustment] ON

INSERT [T_FarmlandAdjustment] ([id],[year],[administratorArea],[verifId],[approvalTime],[startFarmlandArea],[endFarmlandArea],[startFarmlandArableLandArea],[endFarmlandArableLandArea],[taskArea],[adjustBeforeArea],[adjustBeforeArableArea],[adjustAfterArea],[adjustAfterArableArea],[adjustInArea],[adjustInArableArea],[adjustOutArea],[adjustOutArableArea],[userId],[createTime],[isSubmited],[isDeleted],[des]) VALUES ( 3,N'2014',N'1',2,N'0001/1/1 0:00:00',0.0000,0.0000,0.0000,0.0000,100.0000,120.0000,120.0000,0.0000,0.0000,100.0000,100.0000,20.0000,10.0000,1,N'2015/8/9 21:36:31',0,0,N'sadfadsf')

SET IDENTITY_INSERT [T_FarmlandAdjustment] OFF
if exists (select * from sysobjects where id = OBJECT_ID('[T_Icon]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [T_Icon]

CREATE TABLE [T_Icon] (
[id] [int]  IDENTITY (1, 1)  NOT NULL,
[name] [nvarchar]  (100) NOT NULL,
[type] [int]  NOT NULL)

SET IDENTITY_INSERT [T_Icon] ON

INSERT [T_Icon] ([id],[name],[type]) VALUES ( 1,N'icon-setting.png',1)

SET IDENTITY_INSERT [T_Icon] OFF
if exists (select * from sysobjects where id = OBJECT_ID('[T_LandBlock]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [T_LandBlock]

CREATE TABLE [T_LandBlock] (
[id] [int]  IDENTITY (1, 1)  NOT NULL,
[no] [varchar]  (50) NULL,
[name] [varchar]  (50) NULL,
[batchId] [int]  NOT NULL,
[area] [numeric]  (18,4) NULL,
[userId] [int]  NOT NULL,
[createTime] [datetime]  NOT NULL,
[isSubmited] [int]  NOT NULL DEFAULT (0),
[isDeleted] [int]  NOT NULL DEFAULT (0),
[des] [text]  NULL)

SET IDENTITY_INSERT [T_LandBlock] ON

INSERT [T_LandBlock] ([id],[name],[batchId],[area],[userId],[createTime],[isSubmited],[isDeleted]) VALUES ( 36,N'地块1',6,200.0000,1,N'2015/7/26 20:24:51',0,0)

SET IDENTITY_INSERT [T_LandBlock] OFF
if exists (select * from sysobjects where id = OBJECT_ID('[T_LevyCompensate]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [T_LevyCompensate]

CREATE TABLE [T_LevyCompensate] (
[id] [int]  IDENTITY (1, 1)  NOT NULL,
[year] [varchar]  (50) NULL,
[administratorArea] [varchar]  (50) NULL,
[batchId] [int]  NOT NULL,
[landblockId] [int]  NOT NULL,
[batchTypeId] [int]  NULL,
[administrativeArea] [varchar]  (20) NULL,
[town] [varchar]  (40) NULL,
[village] [varchar]  (40) NULL,
[_group] [varchar]  (40) NULL,
[totalPeopleNumber] [numeric]  (15,4) NULL,
[planLevyArea] [numeric]  (15,4) NULL,
[hasLevyArea] [numeric]  (15,4) NULL,
[levyNationalLandArea] [numeric]  (15,4) NULL,
[levyColletLandArea] [numeric]  (15,4) NULL,
[levyPreDeposit] [numeric]  (15,4) NULL,
[countrySocialSecurityFund] [numeric]  (15,4) NULL,
[countryCompensate] [numeric]  (15,4) NULL,
[areaWaterFacilitiesCompensate] [numeric]  (15,4) NULL,
[provHeavyAgriculturalFunds] [numeric]  (15,4) NULL,
[provAdditionalFee] [numeric]  (15,4) NULL,
[provReclamationFee] [numeric]  (15,4) NULL,
[provArableLandTax] [numeric]  (15,4) NULL,
[provSurveyFee] [numeric]  (15,4) NULL,
[provServiceFee] [numeric]  (15,4) NULL,
[userId] [int]  NOT NULL,
[createTime] [datetime]  NOT NULL,
[isSubmited] [int]  NOT NULL DEFAULT (0),
[isDeleted] [int]  NOT NULL DEFAULT (0),
[des] [text]  NULL)

SET IDENTITY_INSERT [T_LevyCompensate] ON

INSERT [T_LevyCompensate] ([id],[year],[administratorArea],[batchId],[landblockId],[batchTypeId],[town],[village],[_group],[totalPeopleNumber],[planLevyArea],[hasLevyArea],[levyNationalLandArea],[levyColletLandArea],[levyPreDeposit],[countrySocialSecurityFund],[countryCompensate],[areaWaterFacilitiesCompensate],[provHeavyAgriculturalFunds],[provAdditionalFee],[provReclamationFee],[provArableLandTax],[provSurveyFee],[provServiceFee],[userId],[createTime],[isSubmited],[isDeleted],[des]) VALUES ( 3,N'2014',N'1',6,36,1,N'a',N'b',N'c',0.0000,100.0000,60.0000,50.0000,50.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,1,N'2015/8/9 20:03:56',0,0,N'ddddda')

SET IDENTITY_INSERT [T_LevyCompensate] OFF
if exists (select * from sysobjects where id = OBJECT_ID('[T_Log]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [T_Log]

CREATE TABLE [T_Log] (
[id] [int]  IDENTITY (1, 1)  NOT NULL,
[type] [varchar]  (50) NOT NULL,
[user_id] [int]  NOT NULL,
[user_name] [varchar]  (50) NOT NULL,
[id_role] [varchar]  (50) NOT NULL,
[ip] [varchar]  (50) NOT NULL,
[create_time] [datetime]  NULL,
[isDeleted] [int]  NOT NULL DEFAULT (0),
[des] [varchar]  (500) NULL)

ALTER TABLE [T_Log] WITH NOCHECK ADD  CONSTRAINT [PK_T_Log] PRIMARY KEY  NONCLUSTERED ( [id] )
SET IDENTITY_INSERT [T_Log] ON

INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 289,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 19:18:22',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 19:18:22 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 290,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 19:19:06',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 19:19:06 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 291,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 19:20:44',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 19:20:44 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 292,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 19:32:33',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 19:32:33 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 293,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 19:34:42',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 19:34:42 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 248,N'【登录】',2,N'jingkou',N'5',N'127.0.0.1',N'2015/8/11 17:09:36',0,N'IP为 127.0.0.1 的用户【jingkou】 身份【区级】在 2015/8/11 17:09:36 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 261,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/11 21:24:37',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/11 21:24:37 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 262,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/11 21:27:21',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/11 21:27:21 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 263,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/11 21:41:32',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/11 21:41:32 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 264,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/11 21:49:23',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/11 21:49:23 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 265,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/11 21:52:43',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/11 21:52:43 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 266,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/11 22:02:08',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/11 22:02:08 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 282,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 18:19:30',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 18:19:30 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 283,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 18:28:23',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 18:28:23 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 284,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 18:39:26',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 18:39:26 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 298,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 22:03:34',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 22:03:34 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 249,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/11 17:21:27',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/11 17:21:27 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 250,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/11 17:40:06',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/11 17:40:06 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 251,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/11 17:56:30',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/11 17:56:30 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 252,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/11 18:04:45',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/11 18:04:45 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 253,N'【登录】',3,N'shiji',N'4',N'127.0.0.1',N'2015/8/11 18:06:05',0,N'IP为 127.0.0.1 的用户【shiji】 身份【市级】在 2015/8/11 18:06:05 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 254,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/11 18:17:52',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/11 18:17:52 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 255,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/11 18:51:31',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/11 18:51:31 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 256,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/11 19:01:01',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/11 19:01:01 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 257,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/11 19:11:50',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/11 19:11:50 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 258,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/11 19:23:20',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/11 19:23:20 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 259,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/11 19:34:04',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/11 19:34:04 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 260,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/11 19:36:45',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/11 19:36:45 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 274,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 15:42:50',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 15:42:50 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 275,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 16:03:12',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 16:03:12 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 276,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 16:08:46',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 16:08:46 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 277,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 16:09:18',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 16:09:18 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 278,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 16:22:18',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 16:22:18 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 279,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 16:27:06',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 16:27:06 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 280,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 16:42:23',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 16:42:23 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 281,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 17:13:11',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 17:13:11 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 285,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 18:42:51',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 18:42:51 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 286,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 19:15:44',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 19:15:44 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 287,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 19:16:39',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 19:16:39 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 288,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 19:17:46',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 19:17:46 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 294,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 20:04:17',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 20:04:17 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 295,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 20:06:25',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 20:06:25 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 296,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 20:07:02',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 20:07:02 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 297,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 20:11:40',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 20:11:40 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 302,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 22:14:27',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 22:14:27 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 303,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 22:24:05',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 22:24:05 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 304,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 22:28:58',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 22:28:58 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 305,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 22:36:01',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 22:36:01 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 306,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 22:44:40',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 22:44:40 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 307,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 22:51:13',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 22:51:13 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 308,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 23:03:12',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 23:03:12 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 299,N'【登录】',3,N'shiji',N'4',N'127.0.0.1',N'2015/8/24 22:06:27',0,N'IP为 127.0.0.1 的用户【shiji】 身份【市级】在 2015/8/24 22:06:27 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 300,N'【登录】',2,N'jingkou',N'5',N'127.0.0.1',N'2015/8/24 22:07:06',0,N'IP为 127.0.0.1 的用户【jingkou】 身份【区级】在 2015/8/24 22:07:06 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 301,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/24 22:08:01',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/24 22:08:01 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 309,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/25 15:24:04',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/25 15:24:04 【登录】')
INSERT [T_Log] ([id],[type],[user_id],[user_name],[id_role],[ip],[create_time],[isDeleted],[des]) VALUES ( 310,N'【登录】',1,N'admin',N'7',N'127.0.0.1',N'2015/8/25 15:28:19',0,N'IP为 127.0.0.1 的用户【admin】 身份【admin】在 2015/8/25 15:28:19 【登录】')

SET IDENTITY_INSERT [T_Log] OFF
if exists (select * from sysobjects where id = OBJECT_ID('[T_Menu]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [T_Menu]

CREATE TABLE [T_Menu] (
[id] [int]  IDENTITY (1, 1)  NOT NULL,
[name] [varchar]  (40) NULL,
[url] [varchar]  (200) NULL,
[icon] [varchar]  (30) NULL,
[description] [varchar]  (200) NULL,
[pid] [int]  NULL,
[isDeleted] [int]  NOT NULL DEFAULT (0),
[_sort] [int]  NULL DEFAULT (1))

ALTER TABLE [T_Menu] WITH NOCHECK ADD  CONSTRAINT [PK_T_Menu] PRIMARY KEY  NONCLUSTERED ( [id] )
SET IDENTITY_INSERT [T_Menu] ON

INSERT [T_Menu] ([id],[name],[icon],[pid],[isDeleted],[_sort]) VALUES ( 5,N'公告管理',N'icon-notice',0,0,7)
INSERT [T_Menu] ([id],[name],[url],[pid],[isDeleted],[_sort]) VALUES ( 6,N'公告列表',N'NoticeMng/NoticeList.aspx',5,0,2)
INSERT [T_Menu] ([id],[name],[icon],[pid],[isDeleted],[_sort]) VALUES ( 9,N'计划下达',N'icon-plan',0,0,1)
INSERT [T_Menu] ([id],[name],[url],[icon],[pid],[isDeleted],[_sort]) VALUES ( 10,N'计划列表',N'../Business/PlanMng/PlanList.aspx',N'icon-man',9,0,2)
INSERT [T_Menu] ([id],[name],[icon],[pid],[isDeleted],[_sort]) VALUES ( 11,N'业务类型管理',N'icon-man',0,0,6)
INSERT [T_Menu] ([id],[name],[url],[pid],[isDeleted],[_sort]) VALUES ( 12,N'计划类型列表',N'../Business/PlanTypeMng/PlanTypeList.aspx',11,0,2)
INSERT [T_Menu] ([id],[name],[url],[icon],[pid],[isDeleted],[_sort]) VALUES ( 15,N'占补平衡类型列表',N'../Business/DemandSupplyBalanceTypeMng/DemandSupplyBalanceTypeList.aspx',N'icon-man',11,0,3)
INSERT [T_Menu] ([id],[name],[url],[icon],[pid],[isDeleted],[_sort]) VALUES ( 16,N'批次类型列表',N'../Business/BatchTypeMng/BatchTypeList.aspx',N'icon-man',11,0,4)
INSERT [T_Menu] ([id],[name],[url],[pid],[isDeleted],[_sort]) VALUES ( 17,N'行政区列表',N'../Business/AdministratorAreaMng/AdministratorAeraList.aspx',11,0,1)
INSERT [T_Menu] ([id],[name],[icon],[pid],[isDeleted],[_sort]) VALUES ( 18,N'系统管理',N'icon-setting',0,0,8)
INSERT [T_Menu] ([id],[name],[url],[pid],[isDeleted],[_sort]) VALUES ( 19,N'菜单列表',N'MenuMng/MenuList.aspx',18,0,1)
INSERT [T_Menu] ([id],[name],[url],[pid],[isDeleted],[_sort]) VALUES ( 20,N'用户列表',N'UserMng/UserList.aspx',18,0,1)
INSERT [T_Menu] ([id],[name],[url],[pid],[isDeleted],[_sort]) VALUES ( 21,N'角色列表',N'RoleMng/RoleList.aspx',18,0,1)
INSERT [T_Menu] ([id],[name],[url],[icon],[pid],[isDeleted],[_sort]) VALUES ( 22,N'占补平衡',N'列表',N'icon-balance',0,0,2)
INSERT [T_Menu] ([id],[name],[icon],[pid],[isDeleted],[_sort]) VALUES ( 23,N'批次管理',N'icon-batch',0,0,3)
INSERT [T_Menu] ([id],[name],[url],[icon],[pid],[isDeleted],[_sort]) VALUES ( 24,N'批次列表',N'../Business/BatchMng/BatchList.aspx',N'icon-man',23,0,1)
INSERT [T_Menu] ([id],[name],[url],[icon],[pid],[isDeleted],[_sort]) VALUES ( 25,N'占补平衡列表',N'../Business/DemandSupplyBalanceMng/DemandSupplyBalanceList.aspx',N'icon-man',22,0,2)
INSERT [T_Menu] ([id],[name],[url],[icon],[pid],[isDeleted],[_sort]) VALUES ( 26,N'计划汇总',N'../Business/PlanMng/PlanSummaryList.aspx',N'icon-man',9,0,4)
INSERT [T_Menu] ([id],[name],[url],[icon],[pid],[isDeleted],[_sort]) VALUES ( 45,N'图标大全',N'../Sys/IconMng/IconList.aspx',N'icon-account',18,1,6)
INSERT [T_Menu] ([id],[name],[icon],[pid],[isDeleted],[_sort]) VALUES ( 28,N'征收补偿管理',N'icon-compasate',0,0,4)
INSERT [T_Menu] ([id],[name],[url],[pid],[isDeleted],[_sort]) VALUES ( 29,N'征收补偿列表',N'../Business/LevyCompensateMng/LevyCompensateList.aspx',28,0,3)
INSERT [T_Menu] ([id],[name],[icon],[pid],[isDeleted],[_sort]) VALUES ( 30,N'基本农田管理',N'icon-farm',0,0,5)
INSERT [T_Menu] ([id],[name],[url],[icon],[pid],[isDeleted],[_sort]) VALUES ( 31,N'基本农田调整',N'../Business/FarmlandAdjustmentMng/FarmlandAdjustmentList.aspx',N'icon-man',30,0,3)
INSERT [T_Menu] ([id],[name],[url],[icon],[pid],[isDeleted],[_sort]) VALUES ( 32,N'基本农田核销',N'../Business/VerifMng/VerifList.aspx',N'icon-man',30,0,1)
INSERT [T_Menu] ([id],[name],[url],[pid],[isDeleted],[_sort]) VALUES ( 33,N'基本农田整合',N'../Business/FarmlandAdjustmentMng/FarmlandStatisticsList.aspx',30,0,4)
INSERT [T_Menu] ([id],[name],[url],[pid],[isDeleted],[_sort]) VALUES ( 34,N'基本农田核销明细',N'../Business/VerifMng/VerifLogList.aspx',30,0,2)
INSERT [T_Menu] ([id],[name],[url],[pid],[isDeleted],[_sort]) VALUES ( 35,N'添加计划',N'../Business/PlanMng/AddPlan.aspx',9,0,1)
INSERT [T_Menu] ([id],[name],[url],[pid],[isDeleted],[_sort]) VALUES ( 37,N'添加占补平衡',N'../Business/DemandSupplyBalanceMng/AddDemandSupplyBalance.aspx',22,0,1)
INSERT [T_Menu] ([id],[name],[url],[pid],[isDeleted],[_sort]) VALUES ( 39,N'添加公告',N'NoticeMng/AddNotice.aspx',5,0,1)
INSERT [T_Menu] ([id],[name],[url],[pid],[isDeleted],[_sort]) VALUES ( 40,N'日志管理',N'../Sys/LogMng/LogList.aspx',18,0,5)
INSERT [T_Menu] ([id],[name],[url],[pid],[isDeleted],[_sort]) VALUES ( 41,N'权限列表',N'PrivilegeMng/PrivilegeList.aspx',18,0,3)
INSERT [T_Menu] ([id],[name],[url],[pid],[isDeleted],[_sort]) VALUES ( 42,N'添加征收补偿',N'../Business/LevyCompensateMng/AddLevyCompensate.aspx',28,0,1)
INSERT [T_Menu] ([id],[name],[url],[icon],[pid],[isDeleted],[_sort]) VALUES ( 43,N'占补平衡汇总',N'../Business/DemandSupplyBalanceMng/DSBalanceSummaryList.aspx',N'icon-man',22,0,5)
INSERT [T_Menu] ([id],[name],[url],[icon],[pid],[isDeleted],[_sort]) VALUES ( 44,N'添加批次',N'../Business/BatchMng/AddBatch.aspx',N'icon-man',23,0,0)

SET IDENTITY_INSERT [T_Menu] OFF
if exists (select * from sysobjects where id = OBJECT_ID('[T_Notice]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [T_Notice]

CREATE TABLE [T_Notice] (
[id] [int]  IDENTITY (1, 1)  NOT NULL,
[title] [varchar]  (50) NULL,
[content] [text]  NULL,
[create_time] [datetime]  NULL,
[isDeleted] [int]  NOT NULL DEFAULT (0),
[admin_id] [int]  NULL)

ALTER TABLE [T_Notice] WITH NOCHECK ADD  CONSTRAINT [PK_T_Notice] PRIMARY KEY  NONCLUSTERED ( [id] )
SET IDENTITY_INSERT [T_Notice] ON

INSERT [T_Notice] ([id],[title],[content],[create_time],[isDeleted],[admin_id]) VALUES ( 1,N'文章',N'<div id="ccontent" class="ccontent">
	<p align="center">
		<img alt="据“全民星探”爆料称文章闹夜店" src="http://gb.cri.cn/mmsource/images/2015/07/03/92/13680299894642846576.jpg" /> 
	</p>
	<p class="pictext" align="center">
		据“全民星探”爆料称文章闹夜店
	</p>
	<p>
		7月3日，有网友爆料称文章和张一山曾现身北京某夜店，狂欢至深夜才回家，照片显示当时两人喝的酩酊大醉，唯有靠互相搀扶才能行走。据该网友透
露，喝至尽兴时，文章邀多人前来捧场，由于现场气氛热烈，两人言行也趋于“大胆”起来。爆料人称，狂欢至尾声，文章和张一山并没有付驻场陪酒的钱，还和夜
店工作人员发生过争执，不过随后，保安便及时赶到上前制止。7月3日中午，文章微博发声：“我还真就只能呵呵一下了！ ”疑回应闹夜店传闻。(新娱)
	</p>
</div>
<br />',N'2015/7/3 19:21:22',0,1)
INSERT [T_Notice] ([id],[title],[content],[create_time],[isDeleted],[admin_id]) VALUES ( 2,N'aa',N'<img src="http://api.map.baidu.com/staticimage?center=121.473704%2C31.230393&zoom=11&width=558&height=360&markers=121.473704%2C31.230393&markerStyles=l%2CA" alt="" /><img src="/js/kindeditor-4.1.10/attached/image/20150705/20150705220041_3854.png" alt="" /><br />',N'2015/7/5 22:00:49',0,1)
INSERT [T_Notice] ([id],[title],[content],[create_time],[isDeleted],[admin_id]) VALUES ( 3,N'bb',N'bb<br />',N'2015/7/18 9:19:50',0,1)
INSERT [T_Notice] ([id],[title],[content],[create_time],[isDeleted],[admin_id]) VALUES ( 4,N'cc',N'ccc',N'2015/8/24 16:11:37',0,1)

SET IDENTITY_INSERT [T_Notice] OFF
if exists (select * from sysobjects where id = OBJECT_ID('[T_Plan]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [T_Plan]

CREATE TABLE [T_Plan] (
[id] [int]  IDENTITY (1, 1)  NOT NULL,
[name] [varchar]  (50) NULL,
[year] [varchar]  (50) NULL,
[administratorArea] [varchar]  (50) NULL,
[planTypeId] [int]  NOT NULL,
[consArea] [numeric]  (18,4) NULL,
[agriArea] [numeric]  (18,4) NULL,
[arabArea] [numeric]  (18,4) NULL,
[issuedQuota] [numeric]  (18,4) NULL,
[remainQuota] [numeric]  (18,4) NULL,
[releaseNo] [varchar]  (50) NULL,
[releaseTime] [date]  NOT NULL,
[userId] [int]  NOT NULL,
[createTime] [datetime]  NOT NULL,
[isSubmited] [int]  NOT NULL DEFAULT (0),
[isDeleted] [int]  NOT NULL DEFAULT (0),
[des] [text]  NULL)

ALTER TABLE [T_Plan] WITH NOCHECK ADD  CONSTRAINT [PK_T_Plan] PRIMARY KEY  NONCLUSTERED ( [id] )
SET IDENTITY_INSERT [T_Plan] ON

INSERT [T_Plan] ([id],[name],[year],[administratorArea],[planTypeId],[consArea],[agriArea],[arabArea],[issuedQuota],[remainQuota],[releaseNo],[releaseTime],[userId],[createTime],[isSubmited],[isDeleted]) VALUES ( 15,N'国家计划1',N'2014',N'2',1,500.0000,300.0000,200.0000,0.0000,0.0000,N'国家计划1',N'2015/7/24 0:00:00',1,N'2015/7/24 21:18:54',1,0)
INSERT [T_Plan] ([id],[name],[year],[administratorArea],[planTypeId],[consArea],[agriArea],[arabArea],[issuedQuota],[remainQuota],[releaseNo],[releaseTime],[userId],[createTime],[isSubmited],[isDeleted]) VALUES ( 16,N'奖励计划1',N'2014',N'1',2,500.0000,300.0000,200.0000,0.0000,0.0000,N'奖励计划1',N'2015/7/23 0:00:00',1,N'2015/7/22 14:17:11',1,0)
INSERT [T_Plan] ([id],[name],[year],[administratorArea],[planTypeId],[consArea],[agriArea],[arabArea],[issuedQuota],[remainQuota],[releaseNo],[releaseTime],[userId],[createTime],[isSubmited],[isDeleted]) VALUES ( 17,N'独立选址计划1',N'2014',N'1',5,500.0000,300.0000,200.0000,0.0000,0.0000,N'1独立选址计划',N'2015/7/25 0:00:00',1,N'2015/7/25 10:20:35',1,0)
INSERT [T_Plan] ([id],[name],[year],[administratorArea],[planTypeId],[consArea],[agriArea],[arabArea],[issuedQuota],[remainQuota],[releaseNo],[releaseTime],[userId],[createTime],[isSubmited],[isDeleted]) VALUES ( 18,N'增减挂钩计划1',N'2014',N'1',6,0.0000,0.0000,0.0000,500.0000,500.0000,N'增减挂钩计划1',N'2015/7/21 0:00:00',1,N'2015/7/22 22:23:15',1,0)
INSERT [T_Plan] ([id],[name],[year],[administratorArea],[planTypeId],[consArea],[agriArea],[arabArea],[issuedQuota],[remainQuota],[releaseNo],[releaseTime],[userId],[createTime],[isSubmited],[isDeleted]) VALUES ( 19,N'国家计划2',N'2014',N'2',1,1000.0000,800.0000,600.0000,0.0000,0.0000,N'国家计划2',N'2015/7/14 0:00:00',1,N'2015/7/26 10:38:18',1,0)
INSERT [T_Plan] ([id],[name],[year],[administratorArea],[planTypeId],[consArea],[agriArea],[arabArea],[issuedQuota],[remainQuota],[releaseNo],[releaseTime],[userId],[createTime],[isSubmited],[isDeleted]) VALUES ( 20,N'国家计划1',N'2014',N'1',1,500.0000,500.0000,500.0000,0.0000,0.0000,N'国家计划1',N'2015/7/14 0:00:00',1,N'2015/7/26 15:59:31',1,0)

SET IDENTITY_INSERT [T_Plan] OFF
if exists (select * from sysobjects where id = OBJECT_ID('[T_PlanType]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [T_PlanType]

CREATE TABLE [T_PlanType] (
[id] [int]  IDENTITY (1, 1)  NOT NULL,
[name] [varchar]  (50) NOT NULL,
[des] [varchar]  (200) NULL)

SET IDENTITY_INSERT [T_PlanType] ON

INSERT [T_PlanType] ([id],[name],[des]) VALUES ( 1,N'国家计划',N'国家计划')
INSERT [T_PlanType] ([id],[name],[des]) VALUES ( 2,N'奖励计划',N'xxx')
INSERT [T_PlanType] ([id],[name]) VALUES ( 3,N'点供计划')
INSERT [T_PlanType] ([id],[name]) VALUES ( 4,N'其他计划')
INSERT [T_PlanType] ([id],[name]) VALUES ( 5,N'独立选址计划')
INSERT [T_PlanType] ([id],[name]) VALUES ( 6,N'增减挂钩计划')

SET IDENTITY_INSERT [T_PlanType] OFF
if exists (select * from sysobjects where id = OBJECT_ID('[T_Privilege]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [T_Privilege]

CREATE TABLE [T_Privilege] (
[id] [int]  IDENTITY (1, 1)  NOT NULL,
[menuId] [int]  NOT NULL,
[name] [varchar]  (50) NOT NULL,
[code] [varchar]  (50) NULL,
[icon] [varchar]  (50) NULL,
[method] [varchar]  (50) NULL,
[createTime] [datetime]  NOT NULL,
[_sort] [int]  NOT NULL DEFAULT (1),
[isDeleted] [int]  NOT NULL DEFAULT (0))

SET IDENTITY_INSERT [T_Privilege] ON

INSERT [T_Privilege] ([id],[menuId],[name],[code],[icon],[method],[createTime],[_sort],[isDeleted]) VALUES ( 4,20,N'200',N'add',N'add',N'add',N'2015/8/8 12:53:30',1,0)
INSERT [T_Privilege] ([id],[menuId],[name],[code],[icon],[method],[createTime],[_sort],[isDeleted]) VALUES ( 2,20,N'300',N'edit',N'edit',N'edit',N'2015/8/8 12:53:40',2,0)
INSERT [T_Privilege] ([id],[menuId],[name],[code],[icon],[method],[createTime],[_sort],[isDeleted]) VALUES ( 3,20,N'400',N'delete',N',delete,aaa',N'add',N'2015/8/8 12:53:34',3,0)
INSERT [T_Privilege] ([id],[menuId],[name],[code],[icon],[method],[createTime],[_sort],[isDeleted]) VALUES ( 5,10,N'200',N'add',N'add',N'add',N'2015/8/8 12:53:54',1,0)
INSERT [T_Privilege] ([id],[menuId],[name],[code],[icon],[method],[createTime],[_sort],[isDeleted]) VALUES ( 6,10,N'300',N'edit',N'edit',N'edit',N'2015/8/8 12:53:51',2,0)
INSERT [T_Privilege] ([id],[menuId],[name],[code],[icon],[method],[createTime],[_sort],[isDeleted]) VALUES ( 7,10,N'400',N'delete',N'delete',N'delete',N'2015/8/8 12:53:46',3,0)
INSERT [T_Privilege] ([id],[menuId],[name],[code],[method],[createTime],[_sort],[isDeleted]) VALUES ( 8,6,N'200',N'add',N'add',N'2015/8/8 13:31:32',1,0)
INSERT [T_Privilege] ([id],[menuId],[name],[code],[method],[createTime],[_sort],[isDeleted]) VALUES ( 9,6,N'300',N'edit',N'edit',N'2015/8/8 13:31:27',2,0)
INSERT [T_Privilege] ([id],[menuId],[name],[code],[method],[createTime],[_sort],[isDeleted]) VALUES ( 10,6,N'400',N'delete',N'delete',N'2015/8/8 13:31:21',3,0)
INSERT [T_Privilege] ([id],[menuId],[name],[code],[method],[createTime],[_sort],[isDeleted]) VALUES ( 11,12,N'200',N'add',N'add',N'2015/8/8 13:23:52',1,0)
INSERT [T_Privilege] ([id],[menuId],[name],[code],[method],[createTime],[_sort],[isDeleted]) VALUES ( 12,12,N'300',N'e',N'e',N'2015/8/8 13:24:03',2,0)
INSERT [T_Privilege] ([id],[menuId],[name],[code],[method],[createTime],[_sort],[isDeleted]) VALUES ( 13,12,N'400',N'd',N'd',N'2015/8/8 13:24:13',3,0)
INSERT [T_Privilege] ([id],[menuId],[name],[code],[method],[createTime],[_sort],[isDeleted]) VALUES ( 14,15,N'200',N'a',N'a',N'2015/8/8 13:24:32',1,0)
INSERT [T_Privilege] ([id],[menuId],[name],[code],[method],[createTime],[_sort],[isDeleted]) VALUES ( 15,15,N'300',N'e',N'e',N'2015/8/8 13:24:39',2,0)
INSERT [T_Privilege] ([id],[menuId],[name],[code],[method],[createTime],[_sort],[isDeleted]) VALUES ( 16,15,N'400',N'd',N'd',N'2015/8/8 13:24:47',3,0)
INSERT [T_Privilege] ([id],[menuId],[name],[code],[method],[createTime],[_sort],[isDeleted]) VALUES ( 17,19,N'200',N'a',N'a',N'2015/8/8 13:24:58',1,0)
INSERT [T_Privilege] ([id],[menuId],[name],[code],[method],[createTime],[_sort],[isDeleted]) VALUES ( 18,19,N'300',N'e',N'e',N'2015/8/8 13:25:05',2,0)
INSERT [T_Privilege] ([id],[menuId],[name],[code],[method],[createTime],[_sort],[isDeleted]) VALUES ( 19,19,N'400',N'd',N'd',N'2015/8/8 13:25:14',3,0)
INSERT [T_Privilege] ([id],[menuId],[name],[code],[method],[createTime],[_sort],[isDeleted]) VALUES ( 20,21,N'200',N'a',N'a',N'2015/8/8 13:25:31',1,0)
INSERT [T_Privilege] ([id],[menuId],[name],[method],[createTime],[_sort],[isDeleted]) VALUES ( 21,21,N'300',N'e',N'2015/8/8 13:25:39',2,0)
INSERT [T_Privilege] ([id],[menuId],[name],[method],[createTime],[_sort],[isDeleted]) VALUES ( 22,21,N'400',N'd',N'2015/8/8 13:25:47',3,0)
INSERT [T_Privilege] ([id],[menuId],[name],[method],[createTime],[_sort],[isDeleted]) VALUES ( 23,41,N'200',N'a',N'2015/8/8 13:26:01',1,0)
INSERT [T_Privilege] ([id],[menuId],[name],[code],[method],[createTime],[_sort],[isDeleted]) VALUES ( 24,41,N'300',N'e',N'e',N'2015/8/8 13:26:09',2,0)
INSERT [T_Privilege] ([id],[menuId],[name],[method],[createTime],[_sort],[isDeleted]) VALUES ( 25,41,N'400',N'd',N'2015/8/8 13:26:18',3,0)
INSERT [T_Privilege] ([id],[menuId],[name],[method],[createTime],[_sort],[isDeleted]) VALUES ( 26,25,N'200',N'a',N'2015/8/8 13:26:28',2,0)
INSERT [T_Privilege] ([id],[menuId],[name],[method],[createTime],[_sort],[isDeleted]) VALUES ( 27,25,N'300',N'e',N'2015/8/8 13:26:34',2,0)
INSERT [T_Privilege] ([id],[menuId],[name],[method],[createTime],[_sort],[isDeleted]) VALUES ( 28,25,N'400',N'd',N'2015/8/8 13:26:41',3,0)
INSERT [T_Privilege] ([id],[menuId],[name],[method],[createTime],[_sort],[isDeleted]) VALUES ( 29,24,N'200',N'a',N'2015/8/8 13:26:53',1,0)
INSERT [T_Privilege] ([id],[menuId],[name],[code],[method],[createTime],[_sort],[isDeleted]) VALUES ( 30,24,N'300',N'e',N'e',N'2015/8/8 13:27:00',2,0)
INSERT [T_Privilege] ([id],[menuId],[name],[method],[createTime],[_sort],[isDeleted]) VALUES ( 31,24,N'400',N'd',N'2015/8/8 13:27:07',3,0)
INSERT [T_Privilege] ([id],[menuId],[name],[code],[method],[createTime],[_sort],[isDeleted]) VALUES ( 32,6,N'100',N'l',N'l',N'2015/8/8 13:55:32',1,0)
INSERT [T_Privilege] ([id],[menuId],[name],[method],[createTime],[_sort],[isDeleted]) VALUES ( 33,10,N'100',N'l',N'2015/8/8 13:55:46',2,0)
INSERT [T_Privilege] ([id],[menuId],[name],[code],[method],[createTime],[_sort],[isDeleted]) VALUES ( 34,25,N'100',N'l',N'l',N'2015/8/8 13:56:02',2,0)
INSERT [T_Privilege] ([id],[menuId],[name],[code],[method],[createTime],[_sort],[isDeleted]) VALUES ( 35,24,N'100',N'l',N'l',N'2015/8/8 13:56:12',2,0)
INSERT [T_Privilege] ([id],[menuId],[name],[code],[method],[createTime],[_sort],[isDeleted]) VALUES ( 36,29,N'100',N'l',N'l',N'2015/8/8 16:12:04',1,0)
INSERT [T_Privilege] ([id],[menuId],[name],[code],[method],[createTime],[_sort],[isDeleted]) VALUES ( 37,29,N'200',N'a',N'a',N'2015/8/8 16:12:10',2,0)
INSERT [T_Privilege] ([id],[menuId],[name],[code],[method],[createTime],[_sort],[isDeleted]) VALUES ( 38,29,N'300',N'e',N'e',N'2015/8/8 16:12:17',2,0)
INSERT [T_Privilege] ([id],[menuId],[name],[code],[method],[createTime],[_sort],[isDeleted]) VALUES ( 39,29,N'400',N'd',N'd',N'2015/8/8 16:12:28',3,0)
INSERT [T_Privilege] ([id],[menuId],[name],[createTime],[_sort],[isDeleted]) VALUES ( 40,10,N'500',N'2015/8/9 16:53:59',4,0)
INSERT [T_Privilege] ([id],[menuId],[name],[createTime],[_sort],[isDeleted]) VALUES ( 41,29,N'1300',N'2015/8/9 17:11:16',7,0)

SET IDENTITY_INSERT [T_Privilege] OFF
if exists (select * from sysobjects where id = OBJECT_ID('[T_Role]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [T_Role]

CREATE TABLE [T_Role] (
[id] [int]  IDENTITY (1, 1)  NOT NULL,
[name] [varchar]  (40) NULL,
[content] [varchar]  (100) NULL,
[description] [varchar]  (200) NULL)

ALTER TABLE [T_Role] WITH NOCHECK ADD  CONSTRAINT [PK_T_Role] PRIMARY KEY  NONCLUSTERED ( [id] )
SET IDENTITY_INSERT [T_Role] ON

INSERT [T_Role] ([id],[name],[content]) VALUES ( 4,N'市级',N'10,210,310,410,110,510,26,35,321,421,25,225,325,425,125,37,43,124,129,1329')
INSERT [T_Role] ([id],[name],[content]) VALUES ( 5,N'区级',N'110,26,321,125,43,23,24,224,324,424,124,28,29,129,229,329,429,1329,42,30,31,32,33,34')
INSERT [T_Role] ([id],[name],[content]) VALUES ( 6,N'公告管理员',N'5,6,206,306,406,106,39')
INSERT [T_Role] ([id],[name]) VALUES ( 7,N'admin')
INSERT [T_Role] ([id],[name],[content]) VALUES ( 8,N'test',N'306,410,510')

SET IDENTITY_INSERT [T_Role] OFF
if exists (select * from sysobjects where id = OBJECT_ID('[T_User]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [T_User]

CREATE TABLE [T_User] (
[id] [int]  IDENTITY (1, 1)  NOT NULL,
[name] [varchar]  (50) NOT NULL,
[loginName] [varchar]  (50) NULL,
[password] [varchar]  (100) NOT NULL,
[telephone] [varchar]  (100) NULL,
[address] [varchar]  (200) NULL,
[birthday] [date]  NULL,
[create_time] [datetime]  NULL,
[isDeleted] [int]  NOT NULL DEFAULT (0),
[isOnline] [int]  NOT NULL DEFAULT (0),
[remarks] [varchar]  (200) NULL)

ALTER TABLE [T_User] WITH NOCHECK ADD  CONSTRAINT [PK_T_User] PRIMARY KEY  NONCLUSTERED ( [id] )
SET IDENTITY_INSERT [T_User] ON

INSERT [T_User] ([id],[name],[loginName],[password],[telephone],[birthday],[create_time],[isDeleted],[isOnline]) VALUES ( 1,N'admin',N'admin',N'5fa285e1bebea6623e33afc4a1fbd5',N'133',N'2015/7/28 0:00:00',N'2015/7/28 20:09:24',0,0)
INSERT [T_User] ([id],[name],[password],[telephone],[address],[birthday],[isDeleted],[isOnline],[remarks]) VALUES ( 2,N'jingkou',N'5fa285e1bebea6623e33afc4a1fbd5',N'1111',N'1111',N'2015/7/25 0:00:00',0,0,N'aaaa')
INSERT [T_User] ([id],[name],[password],[telephone],[birthday],[create_time],[isDeleted],[isOnline]) VALUES ( 3,N'shiji',N'5fa285e1bebea6623e33afc4a1fbd5',N'18252586485',N'2015/7/28 0:00:00',N'2015/7/28 20:22:28',0,0)
INSERT [T_User] ([id],[name],[password],[create_time],[isDeleted],[isOnline]) VALUES ( 4,N'gonggao',N'5fa285e1bebea6623e33afc4a1fbd5',N'2015/8/8 13:59:54',0,0)
INSERT [T_User] ([id],[name],[password],[create_time],[isDeleted],[isOnline]) VALUES ( 5,N'test',N'5fa285e1bebea6623e33afc4a1fbd5',N'2015/8/8 14:02:50',0,0)

SET IDENTITY_INSERT [T_User] OFF
if exists (select * from sysobjects where id = OBJECT_ID('[T_User_Role]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [T_User_Role]

CREATE TABLE [T_User_Role] (
[id] [int]  IDENTITY (1, 1)  NOT NULL,
[id_user] [int]  NULL,
[id_role] [int]  NULL)

ALTER TABLE [T_User_Role] WITH NOCHECK ADD  CONSTRAINT [PK_T_User_Role] PRIMARY KEY  NONCLUSTERED ( [id] )
SET IDENTITY_INSERT [T_User_Role] ON

INSERT [T_User_Role] ([id],[id_user],[id_role]) VALUES ( 19,3,4)
INSERT [T_User_Role] ([id],[id_user],[id_role]) VALUES ( 15,1,7)
INSERT [T_User_Role] ([id],[id_user],[id_role]) VALUES ( 9,2,5)
INSERT [T_User_Role] ([id],[id_user],[id_role]) VALUES ( 20,4,6)
INSERT [T_User_Role] ([id],[id_user],[id_role]) VALUES ( 21,5,8)

SET IDENTITY_INSERT [T_User_Role] OFF
if exists (select * from sysobjects where id = OBJECT_ID('[T_Verif]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [T_Verif]

CREATE TABLE [T_Verif] (
[id] [int]  IDENTITY (1, 1)  NOT NULL,
[year] [varchar]  (10) NOT NULL,
[administratorArea] [varchar]  (20) NOT NULL,
[divisionArea] [numeric]  (15,4) NULL,
[userId] [int]  NOT NULL,
[createTime] [datetime]  NOT NULL,
[isSubmited] [int]  NOT NULL DEFAULT (0),
[isDeleted] [int]  NOT NULL DEFAULT (0),
[des] [text]  NULL)

SET IDENTITY_INSERT [T_Verif] ON

INSERT [T_Verif] ([id],[year],[administratorArea],[divisionArea],[userId],[createTime],[isSubmited],[isDeleted]) VALUES ( 4,N'2015',N'1',300.0000,1,N'2015/8/9 21:12:10',0,0)
INSERT [T_Verif] ([id],[year],[administratorArea],[divisionArea],[userId],[createTime],[isSubmited],[isDeleted]) VALUES ( 2,N'2014',N'1',50000.0000,1,N'2015/8/9 20:30:19',0,0)

SET IDENTITY_INSERT [T_Verif] OFF
if exists (select * from sysobjects where id = OBJECT_ID('[T_Verif_Batch]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [T_Verif_Batch]

CREATE TABLE [T_Verif_Batch] (
[id] [int]  IDENTITY (1, 1)  NOT NULL,
[year] [varchar]  (10) NOT NULL,
[administratorArea] [varchar]  (20) NOT NULL,
[verifId] [int]  NOT NULL,
[batchId] [int]  NOT NULL,
[verifProvArea] [numeric]  (15,4) NULL DEFAULT (0),
[verifProvArableArea] [numeric]  (15,4) NULL DEFAULT (0),
[verifSelfArea] [numeric]  (15,4) NULL DEFAULT (0),
[verifSelfArableArea] [numeric]  (15,4) NULL DEFAULT (0),
[userId] [int]  NOT NULL,
[createTime] [datetime]  NOT NULL,
[isSubmited] [int]  NOT NULL DEFAULT (0),
[isDeleted] [int]  NOT NULL DEFAULT (0),
[des] [text]  NULL)

SET IDENTITY_INSERT [T_Verif_Batch] ON

INSERT [T_Verif_Batch] ([id],[year],[administratorArea],[verifId],[batchId],[verifProvArea],[verifProvArableArea],[verifSelfArea],[verifSelfArableArea],[userId],[createTime],[isSubmited],[isDeleted]) VALUES ( 4,N'2014',N'1',2,3,100.0000,100.0000,100.0000,100.0000,0,N'2015/8/9 20:30:37',0,0)

SET IDENTITY_INSERT [T_Verif_Batch] OFF
