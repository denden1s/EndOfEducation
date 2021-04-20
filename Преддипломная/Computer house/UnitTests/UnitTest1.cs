using Microsoft.VisualStudio.TestTools.UnitTesting;
using Computer_house.OtherClasses;
using System;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void EnterIncorrectIP()
        {
            /*Входные данные: 1234567*/
            /*Желаемый результат: 127.0.0.1*/

            string expectedValue = "127.0.0.1";
            //Фактический результат
            string inputData = "1234567";
            SetupIP setupIP = new SetupIP(inputData);//не работает
            string actual = setupIP.GetIP();

            //Проверка
            Assert.AreEqual(expectedValue, actual);
        }
        [TestMethod]
        public void NoEnterIncorrectIP()
        {
            /*Входные данные: */
            /*Желаемый результат: 127.0.0.1*/

            string expectedValue = "127.0.0.1";
            //Фактический результат
            string inputData = "";
            SetupIP setupIP = new SetupIP(inputData);//не работает
            string actual = setupIP.GetIP();

            //Проверка
            Assert.AreEqual(expectedValue, actual);
        }
        [TestMethod]
        public void EnterCorrectIP()
        {
            /*Входные данные: 192.168.1.1*/
            /*Желаемый результат: 192.168.1.1*/

            string expectedValue = "192.168.1.1";
            //Фактический результат
            string inputData = "192.168.1.1";
            SetupIP setupIP = new SetupIP(inputData);//не работает
            string actual = setupIP.GetIP();

            //Проверка
            Assert.AreEqual(expectedValue, actual);
        }
        [TestMethod]
        public void EnterInCorrectIPAdress()
        {
            /*Входные данные: 192.168.1.1.3*/
            /*Желаемый результат: 127.0.0.1*/

            string expectedValue = "127.0.0.1";
            //Фактический результат
            string inputData = "192.168.1.1.3";
            SetupIP setupIP = new SetupIP(inputData);
            string actual = setupIP.GetIP();

            //Проверка
            Assert.AreEqual(expectedValue, actual);
        }
        [TestMethod]
        public void EnterInCorrectIPWithManyDots()
        {
            /*Входные данные: 192168113...*/
            /*Желаемый результат: 127.0.0.1*/

            string expectedValue = "127.0.0.1";
            //Фактический результат
            string inputData = "192168113...";
            SetupIP setupIP = new SetupIP(inputData);
            string actual = setupIP.GetIP();

            //Проверка
            Assert.AreEqual(expectedValue, actual);
        }

        [TestMethod]
        public void EnterInCorrectIPWithDotsInRandPos()
        {
            /*Входные данные: 1.921681.1.3.*/
            /*Желаемый результат: 127.0.0.1*/

            string expectedValue = "127.0.0.1";
            //Фактический результат
            string inputData = "1.921681.1.3.";
            SetupIP setupIP = new SetupIP(inputData);
            string actual = setupIP.GetIP();

            //Проверка
            Assert.AreEqual(expectedValue, actual);
        }
    }
}
