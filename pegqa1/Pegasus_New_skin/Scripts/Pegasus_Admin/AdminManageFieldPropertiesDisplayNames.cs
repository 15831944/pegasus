/*
* The purpose of this test is to check if the Pegasus Admin Office has the  
* ability to change a field name and have that name change seen on the office
* main level. This test will check if that feature is working properly as well
* as revert any changes back to the default setting when the test is complete. 
*/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;

namespace Pegasus_New_skin.Scripts.Pegasus_Admin
{
    [TestClass]
    public class AdminManageFieldPropertiesDisplayNames : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Admin")]
        [TestCategory("All")]
        [TestCategory("AslamAdmin")]

        public void validateDisplayNames()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username");
            password = oXMLData.getData("settings/Credentials", "password");

            ExecutionLog executionLog = new ExecutionLog();
            LoginHelper loginHelper = new LoginHelper(GetWebDriver());
            AdminManageDisplayNamesHelper manageNames = new AdminManageDisplayNamesHelper(GetWebDriver());


        }
    }
}
