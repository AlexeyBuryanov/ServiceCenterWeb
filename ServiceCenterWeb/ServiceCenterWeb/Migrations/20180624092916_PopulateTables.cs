using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceCenterWeb.Migrations
{
    public partial class PopulateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                -- Мастера по ремонту
                SET IDENTITY_INSERT Masters ON;
                GO
                INSERT INTO Masters(Id, FirstName, LastName, MobilePhone, Reputation) 
                VALUES
                    (1, N'Лидия', N'Давыдова', N'+380951112233', 0),
                    (2, N'Злата', N'Чаурина', N'+380972222233', 5),
                    (3, N'Степанида', N'Львова', N'+380972222444', 20),
                    (4, N'Элла', N'Никифорова', N'+380976666825', 5),
                    (5, N'Сигизмунд', N'Фомов', N'+380972222000', 5),
                    (6, N'Зигфрид', N'Васильев', N'+380972222456', 150),
                    (7, N'Илья', N'Ахметзянов', N'+380972222276', 3),
                    (8, N'Аглая', N'Андреева', N'+380972992233', 0),
                    (9, N'Ювеналий', N'Федотов', N'+380955552233', 0);
                GO
                SET IDENTITY_INSERT Masters OFF;
                GO

                -- Клиенты сервисного центра
                SET IDENTITY_INSERT Clients ON;
                GO
                INSERT INTO Clients(Id, FirstName, LastName, MobilePhone, Reputation) 
                VALUES
                    (1, N'Лилиана', N'Холодкова', N'+380951112233', 0),
                    (2, N'Борислав', N'Герасимов', N'+380972222233', 0),
                    (3, N'Аделаида', N'Фролова', N'+380972222444', 0),
                    (4, N'Вера', N'Пономарёва', N'+380976666825', 0),
                    (5, N'Любомила', N'Петрова', N'+380972222000', 1),
                    (6, N'Никодим', N'Борисов', N'+380972222456', -10),
                    (7, N'Диана', N'Стручкова', N'+380972222276', 20),
                    (8, N'Светозар', N'Недашковский', N'+380972992233', 1),
                    (9, N'Флорентина', N'Журавлёва', N'+380972992233', 1),
                    (10, N'Альбина', N'Сысолятина', N'+380972992233', 1),
                    (11, N'Владилена', N'Федотова', N'+380972992233', 0),
                    (12, N'Владлена', N'Журавель', N'+380972992233', 0),
                    (13, N'Нина', N'Рустамова', N'+380972992233', 0),
                    (14, N'Ярослава', N'Жукова', N'+380972992233', 0),
                    (15, N'Валерьян', N'Городнов', N'+380972992233', 3),
                    (16, N'Клементина', N'Лапина', N'+380972992233', 0),
                    (17, N'Кларисса', N'Баландина', N'+380972992233', 2),
                    (18, N'Луиза', N'Дмитриева', N'+380972992233', -20),
                    (19, N'Вероника', N'Кравченко', N'+380972992233', 0);
                GO
                SET IDENTITY_INSERT Clients OFF;
                GO
                
                -- Производители техники
                SET IDENTITY_INSERT Manufacturers ON;
                GO
                INSERT INTO Manufacturers(Id, Name) 
                VALUES
                    (1, N'MSI'),
                    (2, N'Asus'),
                    (3, N'Lenovo'),
                    (4, N'NVIDIA'),
                    (5, N'Intel'),
                    (6, N'Acer'),
                    (7, N'Dell'),
                    (8, N'Apple'),
                    (9, N'HP'),
                    (10, N'LG'),
                    (11, N'Google'),
                    (12, N'AMD'),
                    (13, N'Неизвестно');
                GO
                SET IDENTITY_INSERT Manufacturers OFF;
                GO

                -- Тип техники
                SET IDENTITY_INSERT TypeTechnics ON;
                GO
                INSERT INTO TypeTechnics(Id, Name) 
                VALUES
                    (1, N'Ноутбук'),
                    (2, N'ПК'),
                    (3, N'Телефон'),
                    (4, N'Смартфон'),
                    (5, N'Роутер'),
                    (6, N'Компьютерное железо');
                GO
                SET IDENTITY_INSERT TypeTechnics OFF;
                GO

                -- Техника
                SET IDENTITY_INSERT Technics ON;
                GO
                INSERT INTO Technics(Id, Name, ManufacturerId, TypeTechnicId) 
                VALUES
                    (1, N'RX480', 1, 6),
                    (2, N'Ideapad Y700', 3, 1),
                    (3, N'Nexus 4', 11, 4),
                    (4, N'GTX1080', 4, 6),
                    (5, N'TL-WR841N', 13, 5),
                    (6, N'RT-N12E', 2, 5),
                    (7, N'Aurora R7', 7, 2),
                    (8, N'Nokia 3310', 13, 3),
                    (9, N'RX580', 1, 6),
                    (10, N'RX470', 1, 6);
                GO
                SET IDENTITY_INSERT Technics OFF;
                GO

                -- Вид работы
                SET IDENTITY_INSERT TypeWorks ON;
                GO
                INSERT INTO TypeWorks(Id, Name) 
                VALUES
                    (1, N'Профилактическое обслуживание'),
                    (2, N'Ремонт с заменой комплектующих'),
                    (3, N'Устранение неисправностей');
                GO
                SET IDENTITY_INSERT TypeWorks OFF;
                GO

                -- Заказы на ремонт
                SET IDENTITY_INSERT Orders ON;
                GO
                INSERT INTO Orders(Id, ClientId, TechnicId, MasterId, OrderDate, IsCompleted, TypeWorkId) 
                VALUES
                    (1,  2,  1,  1, '01.01.2016', 1,  1),
                    (2,  5,  2,  2, '02.06.2017', 1,  1),
                    (3,  8,  3,  3, '03.05.2018', 0,  2),
                    (4,  2,  4,  4, '01.23.2017', 1,  3),
                    (5,  19, 5,  5, '07.17.2018', 0,  1),
                    (6,  15, 6,  6, '03.15.2018', 0,  2),
                    (7,  12, 7,  7, '12.12.2018', 0,  1),
                    (8,  4,  8,  8, '02.25.2018', 0,  3),
                    (9,  2,  9,  9, '05.27.2018', 0,  2),
                    (10, 9,  10, 1, '05.27.2018', 0,  2);
                GO
                SET IDENTITY_INSERT Orders OFF;
                GO
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                TRUNCATE TABLE Orders; GO
                TRUNCATE TABLE TypeWorks; GO
                TRUNCATE TABLE Technics; GO
                TRUNCATE TABLE TypeTechnics; GO
                TRUNCATE TABLE Manufacturers; GO
                TRUNCATE TABLE Clients; GO
                TRUNCATE TABLE Masters; GO
            ");
        }
    }
}
