﻿using ShopTARge22.Core.Domain;
using ShopTARge22.Core.Dto;
using ShopTARge22.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTARge22.RealEstateTest
{
    public class RealEstateTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_AddEmptyRealEstate_WhenReturnResult()
        {
            //Arrange
            RealEstateDto realEstate = new();

            realEstate.Address = "asd";
            realEstate.SizeSqrt = 1024;
            realEstate.RoomCount = 5;
            realEstate.Floor = 3;
            realEstate.BuildingType = "asd";
            realEstate.BuiltInYear = DateTime.Now;
            realEstate.CreatedAt = DateTime.Now;
            realEstate.UpdatedAt = DateTime.Now;

            //Act
            var result = await Svc<IRealEstatesServices>().Create(realEstate);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldNot_GetByIdRealEstate_WhenReturnsNotEqual()
        {
            //Arrange
            Guid wrongGuid = Guid.Parse(Guid.NewGuid().ToString());
            Guid guid = Guid.Parse("173d934d-6446-4a36-a200-515ea63d1795");

            //Act
            await Svc<IRealEstatesServices>().DetailsAsync(guid);

            //Assert
            Assert.NotEqual(wrongGuid, guid);
        }

        [Fact]
        public async Task Should_GetByIdRealEstate_WhenReturnsEqual()
        {
            Guid databaseGuid = Guid.Parse("173d934d-6446-4a36-a200-515ea63d1795");
            Guid guid = Guid.Parse("173d934d-6446-4a36-a200-515ea63d1795");

            await Svc<IRealEstatesServices>().DetailsAsync(guid);

            Assert.Equal(databaseGuid, guid);
        }

        [Fact]
        public async Task Should_DeleteByIdRealEstate_WhenDeleteRealEstate()
        {

            RealEstateDto realEstate = MockRealEstateData();


            var addRealEstate = await Svc<IRealEstatesServices>().Create(realEstate);
            var result = await Svc<IRealEstatesServices>().Delete((Guid)addRealEstate.Id);

            Assert.Equal(result, addRealEstate);
        }

        [Fact]
        public async Task ShouldNot_DeleteByIdRealEstate_WhenDidNotDeleteRealEstate()
        {
            RealEstateDto realEstate = MockRealEstateData();

            var realEstate1 = await Svc<IRealEstatesServices>().Create(realEstate);
            var realEstate2 = await Svc<IRealEstatesServices>().Create(realEstate);

            var result = await Svc<IRealEstatesServices>().Delete((Guid)realEstate2.Id);

            Assert.NotEqual(result.Id, realEstate1.Id);
        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdateData()
        {
            var guid = new Guid("173d934d-6446-4a36-a200-515ea63d1795");

            RealEstateDto dto = MockRealEstateData();

            RealEstate realEstate = new();

            realEstate.Id = Guid.Parse("173d934d-6446-4a36-a200-515ea63d1795");
            realEstate.Address = "Address123";
            realEstate.SizeSqrt = 890;
            realEstate.RoomCount = 9;
            realEstate.Floor = 4;
            realEstate.BuildingType = "qwerty";
            realEstate.BuiltInYear = DateTime.Now.AddYears(1);

            await Svc<IRealEstatesServices>().Update(dto);

            Assert.Equal(realEstate.Id, guid);
            Assert.DoesNotMatch(realEstate.Address, dto.Address);
            Assert.DoesNotMatch(realEstate.Floor.ToString(), dto.Floor.ToString());
            Assert.NotEqual(realEstate.RoomCount, dto.RoomCount);
        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdateDataVersion2()
        {
            RealEstateDto dto = MockRealEstateData();
            var createRealEstate = await Svc<IRealEstatesServices>().Create(dto);

            RealEstateDto update = MockUpdateRealEstateData();
            var result = await Svc<IRealEstatesServices>().Update(update);

            Assert.DoesNotMatch(result.Address, createRealEstate.Address);
            Assert.NotEqual(result.UpdatedAt, createRealEstate.UpdatedAt);
        }

       

        private RealEstateDto MockRealEstateData()
        {
            RealEstateDto realEstate = new()
            {
                Address = "asd",
                SizeSqrt = 123,
                RoomCount = 5,
                Floor = 3,
                BuildingType = "asd",
                BuiltInYear = DateTime.Now,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            return realEstate;
        }

        private RealEstateDto MockUpdateRealEstateData()
        {
            RealEstateDto realEstate = new()
            {
                Address = "asdasd",
                SizeSqrt = 123123,
                RoomCount = 55,
                Floor = 33,
                BuildingType = "asd",
                BuiltInYear = DateTime.Now.AddYears(1),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now.AddYears(1),
            };

            return realEstate;
        }
    }
}