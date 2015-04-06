using System;
using Expedia;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Rhino.Mocks;

namespace ExpediaTest
{
	[TestClass]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[TestInitialize]
		public void TestInitialize()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[TestMethod]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [TestMethod]
        public void TestThatCarDoesGetLocationFromDatabase()
        {
            IDatabase mockDB = mocks.StrictMock<IDatabase>();
            String carLocation = "Terre Haute";
            String anotherCarLocation = "Indy";

            Expect.Call(mockDB.getCarLocation(10)).Return(carLocation);
            Expect.Call(mockDB.getCarLocation(101)).Return(anotherCarLocation);

            mocks.ReplayAll();

            Car target = new Car(10);
            target.Database = mockDB;
            String result;

            result = target.getCarLocation(101);
            Assert.AreEqual(anotherCarLocation, result);

            result = target.getCarLocation(10);
            Assert.AreEqual(carLocation, result);

            mocks.VerifyAll();
        }

        [TestMethod]
        public void TestThatCarDoesGetMileageFromDatabase()
        {
            IDatabase mockDatabase = mocks.StrictMock<IDatabase>();
            Int32 Miles = 100;

            Expect.Call(mockDatabase.Miles).PropertyBehavior();

            mocks.ReplayAll();

            mockDatabase.Miles = Miles;

            var target = new Car(10);
            target.Database = mockDatabase;

            int mileage = target.Mileage;
            Assert.AreEqual(mileage, Miles);

            mocks.VerifyAll();
        }

        [TestMethod]
        public void TestThatBMWCarHasCorrectBasePriceForTenDays()
        {
            var target = ObjectMother.BMW();
            Assert.AreEqual(80, target.getBasePrice());
        }
	}
}
