using System;
using System.Collections.Generic;
using MangementPermission.Service.Model;
using MangementPermission.Service.Service;
using NUnit.Framework;
using Shouldly;

namespace ManagementPermission.Test
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void CreateCompanyStruture_When_Send_Corrected_List_3_Level_Expect_Ok()
        {
            //Arrange
            var input = new List<string>()
            {
                "6",
                "A F",
                "A B",
                "A C E",
                "A",
                "D",
                "A C",
                "A B",
                "CEO",
                "CEO",
                "1",
                "1",
                "1",
                "2"
            };

            var structureService = new StructureService();

            //Act
            var result = structureService.CreateCompanyStruture(input);

            //Assert
            result.ShouldBeOfType<User[]>();
            result.ShouldNotBeEmpty();
            result.Length.ShouldBe(7);
            result.ShouldAllBe(x => x.FullPermissions == null);

            result[0].MemberIndex.Count.ShouldBe(2);
            result[0].MemberIndex[0].ShouldBe(1);
            result[0].MemberIndex[1].ShouldBe(2);
            result[0].Permissions.Length.ShouldBe(2);
            result[0].Permissions[0].ShouldBe("A");
            result[0].Permissions[1].ShouldBe("F");

            result[1].MemberIndex.Count.ShouldBe(3);
            result[1].MemberIndex[0].ShouldBe(3);
            result[1].MemberIndex[1].ShouldBe(4);
            result[1].MemberIndex[2].ShouldBe(5);
            result[1].Permissions.Length.ShouldBe(2);
            result[1].Permissions[0].ShouldBe("A");
            result[1].Permissions[1].ShouldBe("B");

            result[2].MemberIndex.Count.ShouldBe(1);
            result[2].MemberIndex[0].ShouldBe(6);
            result[2].Permissions.Length.ShouldBe(3);
            result[2].Permissions[0].ShouldBe("A");
            result[2].Permissions[1].ShouldBe("C");
            result[2].Permissions[2].ShouldBe("E");

            result[3].MemberIndex.Count.ShouldBe(0);
            result[3].Permissions.Length.ShouldBe(1);
            result[3].Permissions[0].ShouldBe("A");

            result[4].MemberIndex.Count.ShouldBe(0);
            result[4].Permissions.Length.ShouldBe(1);
            result[4].Permissions[0].ShouldBe("D");

            result[5].MemberIndex.Count.ShouldBe(0);
            result[5].Permissions.Length.ShouldBe(2);
            result[5].Permissions[0].ShouldBe("A");
            result[5].Permissions[1].ShouldBe("C");

            result[6].MemberIndex.Count.ShouldBe(0);
            result[6].Permissions.Length.ShouldBe(2);
            result[6].Permissions[0].ShouldBe("A");
            result[6].Permissions[1].ShouldBe("B");
        }

        [Test]
        public void CreateCompanyStruture_When_Send_Corrected_List_With_4_Level_Expect_Ok()
        {
            //Arrange
            var input = new List<string>()
            {
                "6",
                "A F",
                "A B",
                "A C E",
                "A",
                "D",
                "A C",
                "A B",
                "CEO",
                "CEO",
                "1",
                "1",
                "2",
                "3"
            };

            var structureService = new StructureService();

            //Act
            var result = structureService.CreateCompanyStruture(input);

            //Assert
            result.ShouldBeOfType<User[]>();
            result.ShouldNotBeEmpty();
            result.Length.ShouldBe(7);
            result.ShouldAllBe(x => x.FullPermissions == null);

            result[0].MemberIndex.Count.ShouldBe(2);
            result[0].MemberIndex[0].ShouldBe(1);
            result[0].MemberIndex[1].ShouldBe(2);
            result[0].Permissions.Length.ShouldBe(2);
            result[0].Permissions[0].ShouldBe("A");
            result[0].Permissions[1].ShouldBe("F");

            result[1].MemberIndex.Count.ShouldBe(2);
            result[1].MemberIndex[0].ShouldBe(3);
            result[1].MemberIndex[1].ShouldBe(4);
            result[1].Permissions.Length.ShouldBe(2);
            result[1].Permissions[0].ShouldBe("A");
            result[1].Permissions[1].ShouldBe("B");

            result[2].MemberIndex.Count.ShouldBe(1);
            result[2].MemberIndex[0].ShouldBe(5);
            result[2].Permissions.Length.ShouldBe(3);
            result[2].Permissions[0].ShouldBe("A");
            result[2].Permissions[1].ShouldBe("C");
            result[2].Permissions[2].ShouldBe("E");

            result[3].MemberIndex.Count.ShouldBe(1);
            result[3].MemberIndex[0].ShouldBe(6);
            result[3].Permissions.Length.ShouldBe(1);
            result[3].Permissions[0].ShouldBe("A");

            result[4].MemberIndex.Count.ShouldBe(0);
            result[4].Permissions.Length.ShouldBe(1);
            result[4].Permissions[0].ShouldBe("D");

            result[5].MemberIndex.Count.ShouldBe(0);
            result[5].Permissions.Length.ShouldBe(2);
            result[5].Permissions[0].ShouldBe("A");
            result[5].Permissions[1].ShouldBe("C");

            result[6].MemberIndex.Count.ShouldBe(0);
            result[6].Permissions.Length.ShouldBe(2);
            result[6].Permissions[0].ShouldBe("A");
            result[6].Permissions[1].ShouldBe("B");
        }

        [Test]
        public void CreateCompanyStruture_When_Send_Invalid_Quatity_Expect_Error_Message()
        {
            //Arrange
            var input = new List<string>()
            {
                "A",
                "A F",
                "A B",
                "A C E",
                "A",
                "D",
                "A C",
                "A B",
                "CEO",
                "CEO",
                "1",
                "1",
                "1",
                "2"
            };

            var structureService = new StructureService();

            //Act
            var result = Assert.Throws<Exception>(() => structureService.CreateCompanyStruture(input));

            //Assert
            result.ShouldBeOfType<Exception>();
            result.Message.ShouldBe(ErrorMessage.QuantityInvalid);
        }

        [Test]
        public void CreateCompanyStruture_When_Send_Out_Of_Range_Quatity_1_Expect_Error_Message()
        {
            //Arrange
            var input = new List<string>()
            {
                "100000",
                "A F",
                "A B",
                "A C E",
                "A",
                "D",
                "A C",
                "A B",
                "CEO",
                "CEO",
                "1",
                "1",
                "1",
                "2"
            };

            var structureService = new StructureService();

            //Act
            var result = Assert.Throws<Exception>(() => structureService.CreateCompanyStruture(input));

            //Assert
            result.ShouldBeOfType<Exception>();
            result.Message.ShouldBe(ErrorMessage.QuantityOutOfRange);
        }

        [Test]
        public void CreateCompanyStruture_When_Send_Out_Of_Range_Quatity_2_Expect_Error_Message()
        {
            //Arrange
            var input = new List<string>()
            {
                "0",
                "A F",
                "A B",
                "A C E",
                "A",
                "D",
                "A C",
                "A B",
                "CEO",
                "CEO",
                "1",
                "1",
                "1",
                "2"
            };

            var structureService = new StructureService();

            //Act
            var result = Assert.Throws<Exception>(() => structureService.CreateCompanyStruture(input));

            //Assert
            result.ShouldBeOfType<Exception>();
            result.Message.ShouldBe(ErrorMessage.QuantityOutOfRange);
        }

        [Test]
        public void CreateCompanyStruture_When_Send_Not_fix_Quatity_Expect_Error_Message()
        {
            //Arrange
            var input = new List<string>()
            {
                "8",
                "A F",
                "A B",
                "A C E",
                "A",
                "D",
                "A C",
                "A B",
                "CEO",
                "CEO",
                "1",
                "1",
                "1",
                "2"
            };

            var structureService = new StructureService();

            //Act
            var result = Assert.Throws<Exception>(() => structureService.CreateCompanyStruture(input));

            //Assert
            result.ShouldBeOfType<Exception>();
            result.Message.ShouldBe(ErrorMessage.QuantityNotFix);
        }

        [Test]
        public void CreateCompanyStruture_When_Send_Invalid_Permission_Expect_Error_Message()
        {
            //Arrange
            var input = new List<string>()
            {
                "6",
                "",
                "A B",
                "A C E",
                "A",
                "D",
                "A C",
                "A B",
                "CEO",
                "CEO",
                "1",
                "1",
                "1",
                "2"
            };

            var structureService = new StructureService();

            //Act
            var result = Assert.Throws<Exception>(() => structureService.CreateCompanyStruture(input));

            //Assert
            result.ShouldBeOfType<Exception>();
            result.Message.ShouldBe(ErrorMessage.PermissionInvalid);
        }

        [Test]
        public void CreateCompanyStruture_When_Send_Out_Of_Range_Permission_Expect_Error_Message()
        {
            //Arrange
            var input = new List<string>()
            {
                "6",
                "A B C D E F G H I J K L M N O P Q R S T U V W X Y Z A1 B1 C1 D1 E1 F1 G1 H1 I1 J1 K1 L1 M1 N1 O1 P1 Q1 R1 S1 T1 U1 V1 W1 X1 Y1 Z1 A2 B2 C2 D2 E2 F2 G2 H2 I2 J2 K2 L2 M2 N2 O2 P2 Q2 R2 S2 T2 U2 V2 W2 X2 Y2 Z2 A3 B3 C3 D3 E3 F3 G3 H3 I3 J3 K3 L3 M3 N3 O3 P3 Q3 R3 S3 T3 U3 V3 W3 X3 Y3 Z3",
                "A B",
                "A C E",
                "A",
                "D",
                "A C",
                "A B",
                "CEO",
                "CEO",
                "1",
                "1",
                "1",
                "2"
            };

            var structureService = new StructureService();

            //Act
            var result = Assert.Throws<Exception>(() => structureService.CreateCompanyStruture(input));

            //Assert
            result.ShouldBeOfType<Exception>();
            result.Message.ShouldBe(ErrorMessage.PermissionOutOfRange);
        }

        [Test]
        public void CreateCompanyStruture_When_Send_Invalid_Manager_1_Expect_Error_Message()
        {
            //Arrange
            var input = new List<string>()
            {
                "6",
                "A F",
                "A B",
                "A C E",
                "A",
                "D",
                "A C",
                "A B",
                "CEO",
                "CEO",
                "8",
                "1",
                "1",
                "2"
            };

            var structureService = new StructureService();

            //Act
            var result = Assert.Throws<Exception>(() => structureService.CreateCompanyStruture(input));

            //Assert
            result.ShouldBeOfType<Exception>();
            result.Message.ShouldBe(ErrorMessage.ManagerInvalid);
        }

        [Test]
        public void CreateCompanyStruture_When_Send_Invalid_Manager_2_Expect_Error_Message()
        {
            //Arrange
            var input = new List<string>()
            {
                "6",
                "A F",
                "A B",
                "A C E",
                "A",
                "D",
                "A C",
                "A B",
                "CEO",
                "CEO",
                "1",
                "0",
                "1",
                "2"
            };

            var structureService = new StructureService();

            //Act
            var result = Assert.Throws<Exception>(() => structureService.CreateCompanyStruture(input));

            //Assert
            result.ShouldBeOfType<Exception>();
            result.Message.ShouldBe(ErrorMessage.ManagerInvalid);
        }

        [Test]
        public void CreateCompanyStruture_When_Send_Invalid_Manager_3_Expect_Error_Message()
        {
            //Arrange
            var input = new List<string>()
            {
                "6",
                "A F",
                "A B",
                "A C E",
                "A",
                "D",
                "A C",
                "A B",
                "CEO",
                "CEO",
                "1",
                "Manager",
                "1",
                "2"
            };

            var structureService = new StructureService();

            //Act
            var result = Assert.Throws<Exception>(() => structureService.CreateCompanyStruture(input));

            //Assert
            result.ShouldBeOfType<Exception>();
            result.Message.ShouldBe(ErrorMessage.ManagerInvalid);
        }

        [Test]
        public void CreateCompanyStruture_When_Send_Invalid_Manager_4_Expect_Error_Message()
        {
            //Arrange
            var input = new List<string>()
            {
                "6",
                "A F",
                "A B",
                "A C E",
                "A",
                "D",
                "A C",
                "A B",
                "CEO",
                "CEO",
                "1",
                "5",
                "1",
                "2"
            };

            var structureService = new StructureService();

            //Act
            var result = Assert.Throws<Exception>(() => structureService.CreateCompanyStruture(input));

            //Assert
            result.ShouldBeOfType<Exception>();
            result.Message.ShouldBe(ErrorMessage.ManagerInvalid);
        }

        [Test]
        public void CreateCompanyStruture_When_Send_Invalid_Manager_5_Expect_Error_Message()
        {
            //Arrange
            var input = new List<string>()
            {
                "6",
                "A F",
                "A B",
                "A C E",
                "A",
                "D",
                "A C",
                "A B",
                "1",
                "1",
                "1",
                "1",
                "1",
                "2"
            };

            var structureService = new StructureService();

            //Act
            var result = Assert.Throws<Exception>(() => structureService.CreateCompanyStruture(input));

            //Assert
            result.ShouldBeOfType<Exception>();
            result.Message.ShouldBe(ErrorMessage.CEONoMember);
        }

        [Test]
        public void GetPermissionsOfCompany_When_Send_Corrected_Expect_Ok()
        {
            //Arrange
            var input = new User[]
            {
                new User()
                {
                    FullPermissions = null,
                    MemberIndex = new List<int> {1, 2},
                    Permissions = new string[] {"A", "F"}
                },
                new User()
                {
                    FullPermissions = null,
                    MemberIndex = new List<int> {3, 4, 5},
                    Permissions = new string[] {"A", "B"}
                },
                new User()
                {
                    FullPermissions = null,
                    MemberIndex = new List<int> {6},
                    Permissions = new string[] {"A", "C", "E"}
                },
                new User()
                {
                    FullPermissions = null,
                    MemberIndex = new List<int>(),
                    Permissions = new string[] {"A"}
                },
                new User()
                {
                    FullPermissions = null,
                    MemberIndex = new List<int>(),
                    Permissions = new string[] {"D"}
                },
                new User()
                {
                    FullPermissions = null,
                    MemberIndex = new List<int>(),
                    Permissions = new string[] {"A", "C"}
                },
                new User()
                {
                    FullPermissions = null,
                    MemberIndex = new List<int>(),
                    Permissions = new string[] {"A", "B"}
                },
            };

            var structureService = new StructureService();

            //Act
            var result = structureService.GetPermissionsOfCompany(input);

            //Assert
            result.ShouldBeOfType<string[]>();
            result.ShouldNotBeEmpty();
            result.Length.ShouldBe(7);

            result[0].ShouldBe("A, B, C, D, E, F");
            result[1].ShouldBe("A, B, C, D");
            result[2].ShouldBe("A, B, C, E");
            result[3].ShouldBe("A");
            result[4].ShouldBe("D");
            result[5].ShouldBe("A, C");
            result[6].ShouldBe("A, B");
        }
    }
}