
/****** Object:  Table [dbo].[CustomerOrderItems]    Script Date: 15.03.2025 12:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerOrderItems](
	[CustomerOrderItemID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerOrderID] [int] NOT NULL,
	[Product_ID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerOrderItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerOrders]    Script Date: 15.03.2025 12:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerOrders](
	[CustomerOrderID] [int] IDENTITY(1,1) NOT NULL,
	[OrderNumber] [varchar](255) NOT NULL,
	[OrderDate] [date] NOT NULL,
	[CustomerID] [int] NOT NULL,
	[TotalAmount] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerOrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseOrderItems]    Script Date: 15.03.2025 12:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrderItems](
	[PurchaseOrderItemID] [int] IDENTITY(1,1) NOT NULL,
	[PurchaseOrderID] [int] NOT NULL,
	[Product_ID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PurchaseOrderItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseOrders]    Script Date: 15.03.2025 12:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrders](
	[PurchaseOrderID] [int] IDENTITY(1,1) NOT NULL,
	[OrderNumber] [varchar](255) NOT NULL,
	[OrderDate] [date] NOT NULL,
	[SupplierID] [int] NOT NULL,
	[TotalAmount] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[PurchaseOrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ЕдиницыИзмерения]    Script Date: 15.03.2025 12:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ЕдиницыИзмерения](
	[Unit_ID] [int] IDENTITY(1,1) NOT NULL,
	[Название_Единицы_Измерения] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Unit_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ЗоныХранения]    Script Date: 15.03.2025 12:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ЗоныХранения](
	[Storage_Location_ID] [int] IDENTITY(1,1) NOT NULL,
	[Warehouse_ID] [int] NOT NULL,
	[Название_Зоны] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Storage_Location_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Инвентаризации]    Script Date: 15.03.2025 12:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Инвентаризации](
	[Inventory_ID] [int] IDENTITY(1,1) NOT NULL,
	[Дата_Инвентаризации] [date] NOT NULL,
	[User_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Inventory_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[КатегорииТоваров]    Script Date: 15.03.2025 12:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[КатегорииТоваров](
	[Category_ID] [int] IDENTITY(1,1) NOT NULL,
	[Название_Категории] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Category_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Клиенты]    Script Date: 15.03.2025 12:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Клиенты](
	[Customer_ID] [int] IDENTITY(1,1) NOT NULL,
	[Название_Клиента] [varchar](255) NOT NULL,
	[Контактный_Телефон] [varchar](50) NULL,
	[Контактный_Email] [varchar](255) NULL,
	[Адрес] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Customer_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Пользователи]    Script Date: 15.03.2025 12:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Пользователи](
	[User_ID] [int] IDENTITY(1,1) NOT NULL,
	[Имя_Пользователя] [varchar](255) NOT NULL,
	[Хеш_Пароля] [varchar](255) NOT NULL,
	[Соль] [varchar](255) NOT NULL,
	[Role_ID] [int] NOT NULL,
	[Электронная_Почта] [varchar](255) NOT NULL,
	[Двухфакторная_Аутентификация_Включена] [bit] NULL,
	[Секрет_Двухфакторной_Аутентификации] [varchar](255) NULL,
	[Фотография] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[User_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Поставщики]    Script Date: 15.03.2025 12:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Поставщики](
	[Supplier_ID] [int] IDENTITY(1,1) NOT NULL,
	[Название_Поставщика] [varchar](255) NOT NULL,
	[ИНН] [varchar](20) NULL,
	[КПП] [varchar](20) NULL,
	[Контактный_Телефон] [varchar](50) NULL,
	[Контактный_Email] [varchar](255) NULL,
	[Адрес] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Supplier_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ПриходныеНакладные]    Script Date: 15.03.2025 12:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ПриходныеНакладные](
	[Invoice_ID] [int] IDENTITY(1,1) NOT NULL,
	[Номер_Накладной] [varchar](255) NOT NULL,
	[Дата_Накладной] [date] NOT NULL,
	[Supplier_ID] [int] NOT NULL,
	[Сумма_Накладной] [decimal](10, 2) NULL,
	[Файл] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Invoice_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[РасходныеНакладные]    Script Date: 15.03.2025 12:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[РасходныеНакладные](
	[Invoice_ID] [int] IDENTITY(1,1) NOT NULL,
	[Номер_Накладной] [varchar](255) NOT NULL,
	[Дата_Накладной] [date] NOT NULL,
	[Customer_ID] [int] NOT NULL,
	[Сумма_Накладной] [decimal](10, 2) NULL,
	[Файл] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Invoice_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Роли]    Script Date: 15.03.2025 12:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Роли](
	[Role_ID] [int] IDENTITY(1,1) NOT NULL,
	[Название_Роли] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Role_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Склады]    Script Date: 15.03.2025 12:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Склады](
	[Warehouse_ID] [int] IDENTITY(1,1) NOT NULL,
	[Название_Склада] [varchar](255) NOT NULL,
	[Адрес] [varchar](255) NULL,
	[Warehouse_Type_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Warehouse_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[СтрокиИнвентаризации]    Script Date: 15.03.2025 12:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[СтрокиИнвентаризации](
	[Inventory_Item_ID] [int] IDENTITY(1,1) NOT NULL,
	[Inventory_ID] [int] NOT NULL,
	[Product_ID] [int] NOT NULL,
	[Фактическое_Количество] [int] NOT NULL,
	[Учетное_Количество] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Inventory_Item_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[СтрокиПриходнойНакладной]    Script Date: 15.03.2025 12:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[СтрокиПриходнойНакладной](
	[Invoice_Item_ID] [int] IDENTITY(1,1) NOT NULL,
	[Invoice_ID] [int] NOT NULL,
	[Product_ID] [int] NOT NULL,
	[Количество] [int] NOT NULL,
	[Цена] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Invoice_Item_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[СтрокиРасходнойНакладной]    Script Date: 15.03.2025 12:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[СтрокиРасходнойНакладной](
	[Invoice_Item_ID] [int] IDENTITY(1,1) NOT NULL,
	[Invoice_ID] [int] NOT NULL,
	[Product_ID] [int] NOT NULL,
	[Количество] [int] NOT NULL,
	[Цена] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Invoice_Item_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ТипыСкладов]    Script Date: 15.03.2025 12:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ТипыСкладов](
	[Warehouse_Type_ID] [int] IDENTITY(1,1) NOT NULL,
	[Название_Типа] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Warehouse_Type_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Товары]    Script Date: 15.03.2025 12:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Товары](
	[Product_ID] [int] IDENTITY(1,1) NOT NULL,
	[Название_Товара] [varchar](255) NOT NULL,
	[Артикул] [varchar](255) NOT NULL,
	[Штрихкод] [varchar](255) NULL,
	[Category_ID] [int] NOT NULL,
	[Unit_ID] [int] NOT NULL,
	[Цена] [decimal](10, 2) NOT NULL,
	[Серийный_Номер] [varchar](255) NULL,
	[Минимальный_Остаток] [int] NULL,
	[Срок_Годности] [date] NULL,
	[Изображение] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Product_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ТоварыНаСкладе]    Script Date: 15.03.2025 12:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ТоварыНаСкладе](
	[Warehouse_ID] [int] NOT NULL,
	[Product_ID] [int] NOT NULL,
	[Storage_Location_ID] [int] NULL,
	[Количество] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Warehouse_ID] ASC,
	[Product_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[CustomerOrderItems] ON 

INSERT [dbo].[CustomerOrderItems] ([CustomerOrderItemID], [CustomerOrderID], [Product_ID], [Quantity]) VALUES (1, 1, 2, 1)
SET IDENTITY_INSERT [dbo].[CustomerOrderItems] OFF
GO
SET IDENTITY_INSERT [dbo].[CustomerOrders] ON 

INSERT [dbo].[CustomerOrders] ([CustomerOrderID], [OrderNumber], [OrderDate], [CustomerID], [TotalAmount]) VALUES (1, N'123123', CAST(N'2025-02-26' AS Date), 2, CAST(30000.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[CustomerOrders] OFF
GO
SET IDENTITY_INSERT [dbo].[PurchaseOrderItems] ON 

INSERT [dbo].[PurchaseOrderItems] ([PurchaseOrderItemID], [PurchaseOrderID], [Product_ID], [Quantity]) VALUES (1, 1, 1, 3)
SET IDENTITY_INSERT [dbo].[PurchaseOrderItems] OFF
GO
SET IDENTITY_INSERT [dbo].[PurchaseOrders] ON 

INSERT [dbo].[PurchaseOrders] ([PurchaseOrderID], [OrderNumber], [OrderDate], [SupplierID], [TotalAmount]) VALUES (1, N'1', CAST(N'2025-03-12' AS Date), 1, CAST(45000.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[PurchaseOrders] OFF
GO
SET IDENTITY_INSERT [dbo].[ЕдиницыИзмерения] ON 

INSERT [dbo].[ЕдиницыИзмерения] ([Unit_ID], [Название_Единицы_Измерения]) VALUES (2, N'кг')
INSERT [dbo].[ЕдиницыИзмерения] ([Unit_ID], [Название_Единицы_Измерения]) VALUES (3, N'л')
INSERT [dbo].[ЕдиницыИзмерения] ([Unit_ID], [Название_Единицы_Измерения]) VALUES (4, N'м')
INSERT [dbo].[ЕдиницыИзмерения] ([Unit_ID], [Название_Единицы_Измерения]) VALUES (1, N'шт.')
SET IDENTITY_INSERT [dbo].[ЕдиницыИзмерения] OFF
GO
SET IDENTITY_INSERT [dbo].[ЗоныХранения] ON 

INSERT [dbo].[ЗоныХранения] ([Storage_Location_ID], [Warehouse_ID], [Название_Зоны]) VALUES (1, 1, N'Стеллаж A1')
INSERT [dbo].[ЗоныХранения] ([Storage_Location_ID], [Warehouse_ID], [Название_Зоны]) VALUES (2, 1, N'Стеллаж A2')
INSERT [dbo].[ЗоныХранения] ([Storage_Location_ID], [Warehouse_ID], [Название_Зоны]) VALUES (3, 2, N'Стеллаж B1')
SET IDENTITY_INSERT [dbo].[ЗоныХранения] OFF
GO
SET IDENTITY_INSERT [dbo].[Инвентаризации] ON 

INSERT [dbo].[Инвентаризации] ([Inventory_ID], [Дата_Инвентаризации], [User_ID]) VALUES (1, CAST(N'2025-02-28' AS Date), 2)
INSERT [dbo].[Инвентаризации] ([Inventory_ID], [Дата_Инвентаризации], [User_ID]) VALUES (2, CAST(N'2025-03-12' AS Date), 2)
INSERT [dbo].[Инвентаризации] ([Inventory_ID], [Дата_Инвентаризации], [User_ID]) VALUES (3, CAST(N'2025-03-12' AS Date), 2)
INSERT [dbo].[Инвентаризации] ([Inventory_ID], [Дата_Инвентаризации], [User_ID]) VALUES (4, CAST(N'2025-03-12' AS Date), 2)
INSERT [dbo].[Инвентаризации] ([Inventory_ID], [Дата_Инвентаризации], [User_ID]) VALUES (5, CAST(N'2025-03-12' AS Date), 2)
INSERT [dbo].[Инвентаризации] ([Inventory_ID], [Дата_Инвентаризации], [User_ID]) VALUES (6, CAST(N'2025-03-13' AS Date), 9)
INSERT [dbo].[Инвентаризации] ([Inventory_ID], [Дата_Инвентаризации], [User_ID]) VALUES (7, CAST(N'2025-03-13' AS Date), 9)
INSERT [dbo].[Инвентаризации] ([Inventory_ID], [Дата_Инвентаризации], [User_ID]) VALUES (8, CAST(N'2025-03-13' AS Date), 9)
INSERT [dbo].[Инвентаризации] ([Inventory_ID], [Дата_Инвентаризации], [User_ID]) VALUES (9, CAST(N'2025-03-13' AS Date), 9)
INSERT [dbo].[Инвентаризации] ([Inventory_ID], [Дата_Инвентаризации], [User_ID]) VALUES (10, CAST(N'2025-03-13' AS Date), 7)
SET IDENTITY_INSERT [dbo].[Инвентаризации] OFF
GO
SET IDENTITY_INSERT [dbo].[КатегорииТоваров] ON 

INSERT [dbo].[КатегорииТоваров] ([Category_ID], [Название_Категории]) VALUES (2, N'Бытовая техника')
INSERT [dbo].[КатегорииТоваров] ([Category_ID], [Название_Категории]) VALUES (4, N'Одежда')
INSERT [dbo].[КатегорииТоваров] ([Category_ID], [Название_Категории]) VALUES (3, N'Продукты питания')
INSERT [dbo].[КатегорииТоваров] ([Category_ID], [Название_Категории]) VALUES (1, N'Электроника')
SET IDENTITY_INSERT [dbo].[КатегорииТоваров] OFF
GO
SET IDENTITY_INSERT [dbo].[Клиенты] ON 

INSERT [dbo].[Клиенты] ([Customer_ID], [Название_Клиента], [Контактный_Телефон], [Контактный_Email], [Адрес]) VALUES (1, N'Розничный магазин 1', N'8-900-111-11-11', N'shop1@example.com', N'ул. Клиентская, 1')
INSERT [dbo].[Клиенты] ([Customer_ID], [Название_Клиента], [Контактный_Телефон], [Контактный_Email], [Адрес]) VALUES (2, N'Интернет-магазин', N'8-900-222-22-22', N'webshop@example.com', N'ул. Виртуальная, 2')
SET IDENTITY_INSERT [dbo].[Клиенты] OFF
GO
SET IDENTITY_INSERT [dbo].[Пользователи] ON 

INSERT [dbo].[Пользователи] ([User_ID], [Имя_Пользователя], [Хеш_Пароля], [Соль], [Role_ID], [Электронная_Почта], [Двухфакторная_Аутентификация_Включена], [Секрет_Двухфакторной_Аутентификации], [Фотография]) VALUES (1, N'admin', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'MA4LhGtfbFNgrjW8/tRiYg==', 1, N'admin@example.com', 0, NULL, NULL)
INSERT [dbo].[Пользователи] ([User_ID], [Имя_Пользователя], [Хеш_Пароля], [Соль], [Role_ID], [Электронная_Почта], [Двухфакторная_Аутентификация_Включена], [Секрет_Двухфакторной_Аутентификации], [Фотография]) VALUES (2, N'storekeeper1', N'hashed_password', N'salt', 2, N'storekeeper1@example.com', 0, NULL, NULL)
INSERT [dbo].[Пользователи] ([User_ID], [Имя_Пользователя], [Хеш_Пароля], [Соль], [Role_ID], [Электронная_Почта], [Двухфакторная_Аутентификация_Включена], [Секрет_Двухфакторной_Аутентификации], [Фотография]) VALUES (3, N'salesmanager1', N'hashed_password', N'salt', 3, N'salesmanager1@example.com', 0, NULL, NULL)
INSERT [dbo].[Пользователи] ([User_ID], [Имя_Пользователя], [Хеш_Пароля], [Соль], [Role_ID], [Электронная_Почта], [Двухфакторная_Аутентификация_Включена], [Секрет_Двухфакторной_Аутентификации], [Фотография]) VALUES (4, N'accountant1', N'hashed_password', N'salt', 4, N'accountant1@example.com', 0, NULL, NULL)
INSERT [dbo].[Пользователи] ([User_ID], [Имя_Пользователя], [Хеш_Пароля], [Соль], [Role_ID], [Электронная_Почта], [Двухфакторная_Аутентификация_Включена], [Секрет_Двухфакторной_Аутентификации], [Фотография]) VALUES (6, N'1', N'zjrgiKiylbEZLidd6razWbzUp5v6l3/0RfGeOaXvBvwJzns8', N'zjrgiKiylbEZLidd6razWQ==', 1, N'1', 0, NULL, NULL)
INSERT [dbo].[Пользователи] ([User_ID], [Имя_Пользователя], [Хеш_Пароля], [Соль], [Role_ID], [Электронная_Почта], [Двухфакторная_Аутентификация_Включена], [Секрет_Двухфакторной_Аутентификации], [Фотография]) VALUES (7, N'2', N'lq3dtBXD86te/2kgqengbFY5I0FP62XjmOkw8RBCRLUEyDvg', N'lq3dtBXD86te/2kgqengbA==', 2, N'2', 0, NULL, NULL)
INSERT [dbo].[Пользователи] ([User_ID], [Имя_Пользователя], [Хеш_Пароля], [Соль], [Role_ID], [Электронная_Почта], [Двухфакторная_Аутентификация_Включена], [Секрет_Двухфакторной_Аутентификации], [Фотография]) VALUES (8, N'3', N'qXwMQ+5qqMxTPyg22S61AewifGaHo9GNGihgcI8HEYNv1nH3', N'qXwMQ+5qqMxTPyg22S61AQ==', 3, N'3', 0, NULL, NULL)
INSERT [dbo].[Пользователи] ([User_ID], [Имя_Пользователя], [Хеш_Пароля], [Соль], [Role_ID], [Электронная_Почта], [Двухфакторная_Аутентификация_Включена], [Секрет_Двухфакторной_Аутентификации], [Фотография]) VALUES (9, N'4', N'OEDk4WaSleSPjJJCremuoqDytLSJouC6/bKhCak8N4N/TeN+', N'OEDk4WaSleSPjJJCremuog==', 4, N'4', 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Пользователи] OFF
GO
SET IDENTITY_INSERT [dbo].[Поставщики] ON 

INSERT [dbo].[Поставщики] ([Supplier_ID], [Название_Поставщика], [ИНН], [КПП], [Контактный_Телефон], [Контактный_Email], [Адрес]) VALUES (1, N'Поставщик Электроники', N'1234567890', N'123456789', N'8-800-111-11-11', N'electronics@supplier.com', N'ул. Поставщиков, 1')
INSERT [dbo].[Поставщики] ([Supplier_ID], [Название_Поставщика], [ИНН], [КПП], [Контактный_Телефон], [Контактный_Email], [Адрес]) VALUES (2, N'Поставщик Продуктов', N'9876543210', N'987654321', N'8-800-222-22-22', N'food@supplier.com', N'ул. Продуктовая, 2')
SET IDENTITY_INSERT [dbo].[Поставщики] OFF
GO
SET IDENTITY_INSERT [dbo].[ПриходныеНакладные] ON 

INSERT [dbo].[ПриходныеНакладные] ([Invoice_ID], [Номер_Накладной], [Дата_Накладной], [Supplier_ID], [Сумма_Накладной], [Файл]) VALUES (1, N'ПН-001', CAST(N'2025-02-20' AS Date), 1, CAST(450000.00 AS Decimal(10, 2)), NULL)
INSERT [dbo].[ПриходныеНакладные] ([Invoice_ID], [Номер_Накладной], [Дата_Накладной], [Supplier_ID], [Сумма_Накладной], [Файл]) VALUES (2, N'ПН-002', CAST(N'2025-02-22' AS Date), 2, CAST(1550.00 AS Decimal(10, 2)), NULL)
SET IDENTITY_INSERT [dbo].[ПриходныеНакладные] OFF
GO
SET IDENTITY_INSERT [dbo].[РасходныеНакладные] ON 

INSERT [dbo].[РасходныеНакладные] ([Invoice_ID], [Номер_Накладной], [Дата_Накладной], [Customer_ID], [Сумма_Накладной], [Файл]) VALUES (1, N'РН-001', CAST(N'2025-02-25' AS Date), 1, CAST(15050.00 AS Decimal(10, 2)), NULL)
SET IDENTITY_INSERT [dbo].[РасходныеНакладные] OFF
GO
SET IDENTITY_INSERT [dbo].[Роли] ON 

INSERT [dbo].[Роли] ([Role_ID], [Название_Роли]) VALUES (1, N'Администратор')
INSERT [dbo].[Роли] ([Role_ID], [Название_Роли]) VALUES (4, N'Бухгалтер')
INSERT [dbo].[Роли] ([Role_ID], [Название_Роли]) VALUES (2, N'Кладовщик')
INSERT [dbo].[Роли] ([Role_ID], [Название_Роли]) VALUES (3, N'Менеджер по продажам')
SET IDENTITY_INSERT [dbo].[Роли] OFF
GO
SET IDENTITY_INSERT [dbo].[Склады] ON 

INSERT [dbo].[Склады] ([Warehouse_ID], [Название_Склада], [Адрес], [Warehouse_Type_ID]) VALUES (1, N'Главный склад', N'ул. Центральная, 1', 1)
INSERT [dbo].[Склады] ([Warehouse_ID], [Название_Склада], [Адрес], [Warehouse_Type_ID]) VALUES (2, N'Склад №2', N'ул. Промышленная, 5', 2)
INSERT [dbo].[Склады] ([Warehouse_ID], [Название_Склада], [Адрес], [Warehouse_Type_ID]) VALUES (3, N'2', N'2', 2)
SET IDENTITY_INSERT [dbo].[Склады] OFF
GO
SET IDENTITY_INSERT [dbo].[СтрокиИнвентаризации] ON 

INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (1, 1, 1, 9, 10)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (2, 2, 1, 0, 7)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (3, 2, 2, 0, 10)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (4, 2, 3, 0, 9)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (5, 2, 4, 0, 10)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (6, 3, 1, 0, 7)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (7, 3, 2, 0, 10)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (8, 3, 3, 0, 9)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (9, 3, 4, 0, 10)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (10, 4, 1, 0, 7)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (11, 4, 2, 0, 10)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (12, 4, 3, 0, 9)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (13, 4, 4, 0, 10)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (14, 5, 1, 0, 7)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (15, 5, 2, 0, 10)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (16, 5, 3, 0, 9)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (17, 5, 4, 0, 10)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (18, 6, 1, 0, 7)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (19, 6, 2, 0, 10)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (20, 6, 3, 0, 9)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (21, 6, 4, 0, 10)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (22, 7, 1, 0, 7)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (23, 7, 2, 0, 10)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (24, 7, 3, 0, 9)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (25, 7, 4, 0, 10)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (26, 8, 1, 0, 7)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (27, 8, 2, 0, 10)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (28, 8, 3, 0, 9)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (29, 8, 4, 0, 10)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (30, 9, 1, 0, 7)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (31, 9, 2, 0, 10)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (32, 9, 3, 0, 9)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (33, 9, 4, 0, 10)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (34, 10, 1, 0, 7)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (35, 10, 2, 0, 10)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (36, 10, 3, 0, 9)
INSERT [dbo].[СтрокиИнвентаризации] ([Inventory_Item_ID], [Inventory_ID], [Product_ID], [Фактическое_Количество], [Учетное_Количество]) VALUES (37, 10, 4, 0, 10)
SET IDENTITY_INSERT [dbo].[СтрокиИнвентаризации] OFF
GO
SET IDENTITY_INSERT [dbo].[СтрокиПриходнойНакладной] ON 

INSERT [dbo].[СтрокиПриходнойНакладной] ([Invoice_Item_ID], [Invoice_ID], [Product_ID], [Количество], [Цена]) VALUES (1, 1, 1, 10, CAST(15000.00 AS Decimal(10, 2)))
INSERT [dbo].[СтрокиПриходнойНакладной] ([Invoice_Item_ID], [Invoice_ID], [Product_ID], [Количество], [Цена]) VALUES (2, 1, 2, 10, CAST(30000.00 AS Decimal(10, 2)))
INSERT [dbo].[СтрокиПриходнойНакладной] ([Invoice_Item_ID], [Invoice_ID], [Product_ID], [Количество], [Цена]) VALUES (3, 2, 3, 10, CAST(50.00 AS Decimal(10, 2)))
INSERT [dbo].[СтрокиПриходнойНакладной] ([Invoice_Item_ID], [Invoice_ID], [Product_ID], [Количество], [Цена]) VALUES (4, 2, 4, 10, CAST(80.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[СтрокиПриходнойНакладной] OFF
GO
SET IDENTITY_INSERT [dbo].[СтрокиРасходнойНакладной] ON 

INSERT [dbo].[СтрокиРасходнойНакладной] ([Invoice_Item_ID], [Invoice_ID], [Product_ID], [Количество], [Цена]) VALUES (1, 1, 1, 1, CAST(15000.00 AS Decimal(10, 2)))
INSERT [dbo].[СтрокиРасходнойНакладной] ([Invoice_Item_ID], [Invoice_ID], [Product_ID], [Количество], [Цена]) VALUES (2, 1, 3, 1, CAST(50.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[СтрокиРасходнойНакладной] OFF
GO
SET IDENTITY_INSERT [dbo].[ТипыСкладов] ON 

INSERT [dbo].[ТипыСкладов] ([Warehouse_Type_ID], [Название_Типа]) VALUES (1, N'Основной')
INSERT [dbo].[ТипыСкладов] ([Warehouse_Type_ID], [Название_Типа]) VALUES (2, N'Временный')
INSERT [dbo].[ТипыСкладов] ([Warehouse_Type_ID], [Название_Типа]) VALUES (3, N'Розничный')
SET IDENTITY_INSERT [dbo].[ТипыСкладов] OFF
GO
SET IDENTITY_INSERT [dbo].[Товары] ON 

INSERT [dbo].[Товары] ([Product_ID], [Название_Товара], [Артикул], [Штрихкод], [Category_ID], [Unit_ID], [Цена], [Серийный_Номер], [Минимальный_Остаток], [Срок_Годности], [Изображение]) VALUES (1, N'Смартфон', N'SM-001', N'1234567890123', 1, 1, CAST(15000.00 AS Decimal(10, 2)), N'SN12345', 10, NULL, NULL)
INSERT [dbo].[Товары] ([Product_ID], [Название_Товара], [Артикул], [Штрихкод], [Category_ID], [Unit_ID], [Цена], [Серийный_Номер], [Минимальный_Остаток], [Срок_Годности], [Изображение]) VALUES (2, N'Телевизор', N'TV-001', N'9876543210987', 1, 1, CAST(30000.00 AS Decimal(10, 2)), N'SN67890', 5, NULL, NULL)
INSERT [dbo].[Товары] ([Product_ID], [Название_Товара], [Артикул], [Штрихкод], [Category_ID], [Unit_ID], [Цена], [Серийный_Номер], [Минимальный_Остаток], [Срок_Годности], [Изображение]) VALUES (3, N'Хлеб', N'BR-001', N'2345678901234', 3, 1, CAST(50.00 AS Decimal(10, 2)), NULL, 20, CAST(N'2025-03-01' AS Date), NULL)
INSERT [dbo].[Товары] ([Product_ID], [Название_Товара], [Артикул], [Штрихкод], [Category_ID], [Unit_ID], [Цена], [Серийный_Номер], [Минимальный_Остаток], [Срок_Годности], [Изображение]) VALUES (4, N'Молоко', N'MLK-001', N'8765432109876', 3, 3, CAST(80.00 AS Decimal(10, 2)), NULL, 15, CAST(N'2025-03-10' AS Date), NULL)
SET IDENTITY_INSERT [dbo].[Товары] OFF
GO
INSERT [dbo].[ТоварыНаСкладе] ([Warehouse_ID], [Product_ID], [Storage_Location_ID], [Количество]) VALUES (1, 1, 1, 7)
INSERT [dbo].[ТоварыНаСкладе] ([Warehouse_ID], [Product_ID], [Storage_Location_ID], [Количество]) VALUES (1, 2, 2, 10)
INSERT [dbo].[ТоварыНаСкладе] ([Warehouse_ID], [Product_ID], [Storage_Location_ID], [Количество]) VALUES (1, 3, 1, 9)
INSERT [dbo].[ТоварыНаСкладе] ([Warehouse_ID], [Product_ID], [Storage_Location_ID], [Количество]) VALUES (1, 4, 2, 10)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__ЕдиницыИ__DA10675660384A5D]    Script Date: 15.03.2025 12:32:01 ******/
ALTER TABLE [dbo].[ЕдиницыИзмерения] ADD UNIQUE NONCLUSTERED 
(
	[Название_Единицы_Измерения] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Категори__49DA1BB001ED396F]    Script Date: 15.03.2025 12:32:01 ******/
ALTER TABLE [dbo].[КатегорииТоваров] ADD UNIQUE NONCLUSTERED 
(
	[Название_Категории] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Пользова__0E10CAC442FFA303]    Script Date: 15.03.2025 12:32:01 ******/
ALTER TABLE [dbo].[Пользователи] ADD UNIQUE NONCLUSTERED 
(
	[Электронная_Почта] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Пользова__E5B62B754353CD5A]    Script Date: 15.03.2025 12:32:01 ******/
ALTER TABLE [dbo].[Пользователи] ADD UNIQUE NONCLUSTERED 
(
	[Имя_Пользователя] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Роли__74399AE1F5FB2F8E]    Script Date: 15.03.2025 12:32:01 ******/
ALTER TABLE [dbo].[Роли] ADD UNIQUE NONCLUSTERED 
(
	[Название_Роли] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Товары__2AF1CF03F400FBD0]    Script Date: 15.03.2025 12:32:01 ******/
ALTER TABLE [dbo].[Товары] ADD UNIQUE NONCLUSTERED 
(
	[Штрихкод] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Товары__F333AABFC44A963F]    Script Date: 15.03.2025 12:32:01 ******/
ALTER TABLE [dbo].[Товары] ADD UNIQUE NONCLUSTERED 
(
	[Артикул] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CustomerOrders] ADD  DEFAULT ((0)) FOR [TotalAmount]
GO
ALTER TABLE [dbo].[PurchaseOrders] ADD  DEFAULT ((0)) FOR [TotalAmount]
GO
ALTER TABLE [dbo].[Пользователи] ADD  DEFAULT ((0)) FOR [Двухфакторная_Аутентификация_Включена]
GO
ALTER TABLE [dbo].[ПриходныеНакладные] ADD  DEFAULT ((0)) FOR [Сумма_Накладной]
GO
ALTER TABLE [dbo].[РасходныеНакладные] ADD  DEFAULT ((0)) FOR [Сумма_Накладной]
GO
ALTER TABLE [dbo].[CustomerOrderItems]  WITH CHECK ADD FOREIGN KEY([CustomerOrderID])
REFERENCES [dbo].[CustomerOrders] ([CustomerOrderID])
GO
ALTER TABLE [dbo].[CustomerOrderItems]  WITH CHECK ADD FOREIGN KEY([Product_ID])
REFERENCES [dbo].[Товары] ([Product_ID])
GO
ALTER TABLE [dbo].[CustomerOrders]  WITH CHECK ADD FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Клиенты] ([Customer_ID])
GO
ALTER TABLE [dbo].[PurchaseOrderItems]  WITH CHECK ADD FOREIGN KEY([Product_ID])
REFERENCES [dbo].[Товары] ([Product_ID])
GO
ALTER TABLE [dbo].[PurchaseOrderItems]  WITH CHECK ADD FOREIGN KEY([PurchaseOrderID])
REFERENCES [dbo].[PurchaseOrders] ([PurchaseOrderID])
GO
ALTER TABLE [dbo].[PurchaseOrders]  WITH CHECK ADD FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Поставщики] ([Supplier_ID])
GO
ALTER TABLE [dbo].[ЗоныХранения]  WITH CHECK ADD FOREIGN KEY([Warehouse_ID])
REFERENCES [dbo].[Склады] ([Warehouse_ID])
GO
ALTER TABLE [dbo].[Инвентаризации]  WITH CHECK ADD FOREIGN KEY([User_ID])
REFERENCES [dbo].[Пользователи] ([User_ID])
GO
ALTER TABLE [dbo].[Пользователи]  WITH CHECK ADD FOREIGN KEY([Role_ID])
REFERENCES [dbo].[Роли] ([Role_ID])
GO
ALTER TABLE [dbo].[ПриходныеНакладные]  WITH CHECK ADD FOREIGN KEY([Supplier_ID])
REFERENCES [dbo].[Поставщики] ([Supplier_ID])
GO
ALTER TABLE [dbo].[РасходныеНакладные]  WITH CHECK ADD FOREIGN KEY([Customer_ID])
REFERENCES [dbo].[Клиенты] ([Customer_ID])
GO
ALTER TABLE [dbo].[Склады]  WITH CHECK ADD FOREIGN KEY([Warehouse_Type_ID])
REFERENCES [dbo].[ТипыСкладов] ([Warehouse_Type_ID])
GO
ALTER TABLE [dbo].[СтрокиИнвентаризации]  WITH CHECK ADD FOREIGN KEY([Inventory_ID])
REFERENCES [dbo].[Инвентаризации] ([Inventory_ID])
GO
ALTER TABLE [dbo].[СтрокиИнвентаризации]  WITH CHECK ADD FOREIGN KEY([Product_ID])
REFERENCES [dbo].[Товары] ([Product_ID])
GO
ALTER TABLE [dbo].[СтрокиПриходнойНакладной]  WITH CHECK ADD FOREIGN KEY([Invoice_ID])
REFERENCES [dbo].[ПриходныеНакладные] ([Invoice_ID])
GO
ALTER TABLE [dbo].[СтрокиПриходнойНакладной]  WITH CHECK ADD FOREIGN KEY([Product_ID])
REFERENCES [dbo].[Товары] ([Product_ID])
GO
ALTER TABLE [dbo].[СтрокиРасходнойНакладной]  WITH CHECK ADD FOREIGN KEY([Invoice_ID])
REFERENCES [dbo].[РасходныеНакладные] ([Invoice_ID])
GO
ALTER TABLE [dbo].[СтрокиРасходнойНакладной]  WITH CHECK ADD FOREIGN KEY([Product_ID])
REFERENCES [dbo].[Товары] ([Product_ID])
GO
ALTER TABLE [dbo].[Товары]  WITH CHECK ADD FOREIGN KEY([Category_ID])
REFERENCES [dbo].[КатегорииТоваров] ([Category_ID])
GO
ALTER TABLE [dbo].[Товары]  WITH CHECK ADD FOREIGN KEY([Unit_ID])
REFERENCES [dbo].[ЕдиницыИзмерения] ([Unit_ID])
GO
ALTER TABLE [dbo].[ТоварыНаСкладе]  WITH CHECK ADD FOREIGN KEY([Product_ID])
REFERENCES [dbo].[Товары] ([Product_ID])
GO
ALTER TABLE [dbo].[ТоварыНаСкладе]  WITH CHECK ADD FOREIGN KEY([Storage_Location_ID])
REFERENCES [dbo].[ЗоныХранения] ([Storage_Location_ID])
GO
ALTER TABLE [dbo].[ТоварыНаСкладе]  WITH CHECK ADD FOREIGN KEY([Warehouse_ID])
REFERENCES [dbo].[Склады] ([Warehouse_ID])
GO
USE [master]
GO
ALTER DATABASE [SkladUchet] SET  READ_WRITE 
GO
