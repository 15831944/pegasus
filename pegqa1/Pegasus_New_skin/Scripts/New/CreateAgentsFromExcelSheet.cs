using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using PegasusTests.PageHelper;
//using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CreateAgentsFromExcelSheet : DriverTestCase
    {
        [TestMethod]
        [TestCategory("CreateAgents")]
        [TestCategory("New")]

        public void createAgentsFromExcelSheet()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");
            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var agents_EmployeesHelper = new Agents_EmployeesHelper(GetWebDriver());
            var agent_1099SalesAgentHelper = new Agent_1099SalesAgentHelper(GetWebDriver());
            var agents_PartnerAgentsHelper = new Agents_PartnerAgentsHelper(GetWebDriver());
            var agents_PartnerAssociationHelper = new Agents_PartnerAssociationHelper(GetWebDriver());


            executionLog.Log("CreateAgentsFromExcelSheet", "Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("CreateAgentsFromExcelSheet", "Verify Page title");
            VerifyTitle("Dashboard");

            executionLog.Log("CreateAgentsFromExcelSheet", "Read Data from Excel File and Create Agents");

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range xlrange;

            string xlString;
            string xlString1;
            double xlDouble;
            int xlRowCnt = 0;
            int xlColCnt = 0;

            xlApp = new Excel.Application();
            //Open Excel file
            var locFile = GetPathToFile() + "OfficeAgents.xlsx";
            //var updated_path = locFile.Replace("Files", "bin");
            xlWorkBook = xlApp.Workbooks.Open(locFile);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            //This gives the used cells in the sheet
            xlrange = xlWorkSheet.UsedRange;
            Console.WriteLine("Row count is  " + xlrange.Rows.Count);

            for (xlRowCnt = 2; xlRowCnt <= xlrange.Rows.Count; xlRowCnt++)
            {
                // Find type of Agent
                xlString = (string)(xlrange.Cells[xlRowCnt, 1] as Excel.Range).Value2;
                if (xlString == "Employee")
                {
                    GetWebDriver().Navigate().GoToUrl("https://www.mypegasuscrm.com/newthemecorp/" + username[0] + "/employees/create");
                    agents_EmployeesHelper.WaitForWorkAround(3000);

                    //Employee First Name
                    xlString = (string)(xlrange.Cells[xlRowCnt, 2] as Excel.Range).Value2;
                    agents_EmployeesHelper.TypeText("FirstNAME", xlString);

                    //Employee Last Name
                    xlString = (string)(xlrange.Cells[xlRowCnt, 3] as Excel.Range).Value2;
                    agents_EmployeesHelper.TypeText("LastName", xlString);

                    //Employee Birthday
                    xlString = (string)(xlrange.Cells[xlRowCnt, 4] as Excel.Range).Value2;
                    agents_EmployeesHelper.TypeText("BirthDay", xlString);

                    //Employee eAddress Type
                    xlString = (string)(xlrange.Cells[xlRowCnt, 6] as Excel.Range).Value2;
                    agents_EmployeesHelper.SelectByText("eAddressType", xlString);
                    agents_EmployeesHelper.WaitForWorkAround(1000);

                    //Employee eAddress Label
                    xlString = (string)(xlrange.Cells[xlRowCnt, 7] as Excel.Range).Value2;
                    agents_EmployeesHelper.SelectByText("eAddressLebel", xlString);

                    //Employee eAddress
                    xlString = (string)(xlrange.Cells[xlRowCnt, 8] as Excel.Range).Value2;
                    agents_EmployeesHelper.TypeText("eAddress", xlString);

                    //Employee Username
                    xlString = (string)(xlrange.Cells[xlRowCnt, 9] as Excel.Range).Value2;
                    agents_EmployeesHelper.TypeText("UserName", xlString);

                    //Employee Password
                    agents_EmployeesHelper.ClickElement("AutoGenPswdChkBox");
                    agents_EmployeesHelper.WaitForWorkAround(1000);
                    try
                    {
                        xlDouble = (double)(xlrange.Cells[xlRowCnt, 10] as Excel.Range).Value2;
                        xlString = "" + xlDouble;
                        agents_EmployeesHelper.TypeText("Password", xlString);
                    }
                    catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                    {
                        xlString = (string)(xlrange.Cells[xlRowCnt, 10] as Excel.Range).Value2;
                        agents_EmployeesHelper.TypeText("Password", xlString);
                    }

                    //Employee Avatar
                    xlString = (string)(xlrange.Cells[xlRowCnt, 11] as Excel.Range).Value2;
                    int num = agents_EmployeesHelper.XpathCount("//form[@id='EmployeeCreateEmployeeForm']/div[3]/div[5]/div/div/div[2]/div/div[3]/ul/li");
                    Console.WriteLine("num="+num);
                    for (int i = 1; i <= num; i++)
                    {
                        string name = agents_EmployeesHelper.GetText("//form[@id='EmployeeCreateEmployeeForm']/div[3]/div[5]/div/div/div[2]/div/div[3]/ul/li[" + i + "]/label/span");
                        if (name == xlString)
                        {
                            agents_EmployeesHelper.Click("//form[@id='EmployeeCreateEmployeeForm']/div[3]/div[5]/div/div/div[2]/div/div[3]/ul/li[" + i + "]/input");
                            agents_EmployeesHelper.WaitForWorkAround(1000);
                            break;
                        }
                        else
                            continue;
                    }

                    //Click on Save
                    agents_EmployeesHelper.ClickElement("SaveEmployee");
                    agents_EmployeesHelper.WaitForWorkAround(3000);
                    if (GetWebDriver().PageSource.Contains("This username already taken") == true)
                        continue;
                }

                else if (xlString == "Sales Agent")
                {
                    GetWebDriver().Navigate().GoToUrl("https://www.mypegasuscrm.com/newthemecorp/" + username[0] + "/sales_agents/create");
                    agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                    //1099 Sales Agent First Name
                    xlString = (string)(xlrange.Cells[xlRowCnt, 2] as Excel.Range).Value2;
                    agent_1099SalesAgentHelper.TypeText("FirstNAME", xlString);

                    //1099 Sales Agent Last Name
                    xlString = (string)(xlrange.Cells[xlRowCnt, 3] as Excel.Range).Value2;
                    agent_1099SalesAgentHelper.TypeText("LastName", xlString);

                    //1099 Sales Agent Birthday
                    xlString = (string)(xlrange.Cells[xlRowCnt, 4] as Excel.Range).Value2;
                    agent_1099SalesAgentHelper.TypeText("BirthDay", xlString);

                    //1099 Sales Agent eAddress Type
                    xlString = (string)(xlrange.Cells[xlRowCnt, 6] as Excel.Range).Value2;
                    agent_1099SalesAgentHelper.SelectByText("eAddressType", xlString);
                    agent_1099SalesAgentHelper.WaitForWorkAround(1000);

                    //1099 Sales Agent eAddress Label
                    xlString = (string)(xlrange.Cells[xlRowCnt, 7] as Excel.Range).Value2;
                    agent_1099SalesAgentHelper.SelectByText("eAddressLebel", xlString);

                    //1099 Sales Agent eAddress
                    xlString = (string)(xlrange.Cells[xlRowCnt, 8] as Excel.Range).Value2;
                    agent_1099SalesAgentHelper.TypeText("eAddress", xlString);

                    //1099 Sales Agent Username
                    xlString = (string)(xlrange.Cells[xlRowCnt, 9] as Excel.Range).Value2;
                    agent_1099SalesAgentHelper.TypeText("UserName", xlString);

                    //1099 Sales Agent Password
                    agent_1099SalesAgentHelper.ClickElement("AutoGenPswd");
                    agent_1099SalesAgentHelper.WaitForWorkAround(1000);
                    try
                    {
                        xlDouble = (double)(xlrange.Cells[xlRowCnt, 10] as Excel.Range).Value2;
                        xlString = "" + xlDouble;
                        agent_1099SalesAgentHelper.TypeText("Password", xlString);
                    }
                    catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                    {
                        xlString = (string)(xlrange.Cells[xlRowCnt, 10] as Excel.Range).Value2;
                        agent_1099SalesAgentHelper.TypeText("Password", xlString);
                    }

                    //1099 Sales Agent Avatar
                    xlString = (string)(xlrange.Cells[xlRowCnt, 11] as Excel.Range).Value2;
                    int num = agent_1099SalesAgentHelper.XpathCount("//form[@id='EmployeeCreateSalesUserForm']/div[3]/div[3]/div/div/div[2]/div/div[3]/ul/li");
                    for (int i = 1; i <= num; i++)
                    {
                        string name = agent_1099SalesAgentHelper.GetText("//form[@id='EmployeeCreateSalesUserForm']/div[3]/div[3]/div/div/div[2]/div/div[3]/ul/li[" + i + "]/label/span");
                        if (name == xlString)
                        {
                            agent_1099SalesAgentHelper.Click("//form[@id='EmployeeCreateSalesUserForm']/div[3]/div[3]/div/div/div[2]/div/div[3]/ul/li[" + i + "]/input");
                            break;
                        }
                        else
                            continue;
                    }

                    //Click on Save
                    agent_1099SalesAgentHelper.ClickElement("SaveSaleAgent");
                    agent_1099SalesAgentHelper.WaitForWorkAround(3000);
                    if (GetWebDriver().PageSource.Contains("This username already taken") == true)
                        continue;
                }

                else if (xlString == "Referral Agent")
                {
                    GetWebDriver().Navigate().GoToUrl("https://www.mypegasuscrm.com/newthemecorp/" + username[0] + "/partners/agent/create");
                    agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                    //Partner Agent First Name
                    xlString = (string)(xlrange.Cells[xlRowCnt, 2] as Excel.Range).Value2;
                    agents_PartnerAgentsHelper.TypeText("FirstName", xlString);

                    //Partner Agent Last Name
                    xlString = (string)(xlrange.Cells[xlRowCnt, 3] as Excel.Range).Value2;
                    agents_PartnerAgentsHelper.TypeText("LastName", xlString);

                    //Partner Agent Birthday
                    xlString = (string)(xlrange.Cells[xlRowCnt, 4] as Excel.Range).Value2;
                    agents_PartnerAgentsHelper.TypeText("BirthDay", xlString);

                    //Partner Agent eAddress Type
                    xlString = (string)(xlrange.Cells[xlRowCnt, 6] as Excel.Range).Value2;
                    agents_PartnerAgentsHelper.SelectByText("eAddressType", xlString);
                    agents_PartnerAgentsHelper.WaitForWorkAround(1000);

                    //Partner Agent eAddress Label
                    xlString = (string)(xlrange.Cells[xlRowCnt, 7] as Excel.Range).Value2;
                    agents_PartnerAgentsHelper.SelectByText("eAddressLebel", xlString);

                    //Partner Agent eAddress
                    xlString = (string)(xlrange.Cells[xlRowCnt, 8] as Excel.Range).Value2;
                    agents_PartnerAgentsHelper.TypeText("eAddress", xlString);

                    //Partner Agent Username
                    xlString = (string)(xlrange.Cells[xlRowCnt, 9] as Excel.Range).Value2;
                    agents_PartnerAgentsHelper.TypeText("UserName", xlString);

                    //Partner Agent Password
                    agents_PartnerAgentsHelper.ClickElement("AutoGenPassword");
                    agents_PartnerAgentsHelper.WaitForWorkAround(1000);
                    try
                    {
                        xlDouble = (double)(xlrange.Cells[xlRowCnt, 10] as Excel.Range).Value2;
                        xlString = "" + xlDouble;
                        agents_PartnerAgentsHelper.TypeText("UserPassword", xlString);
                    }
                    catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                    {
                        xlString = (string)(xlrange.Cells[xlRowCnt, 10] as Excel.Range).Value2;
                        agents_PartnerAgentsHelper.TypeText("UserPassword", xlString);
                    }

                    //Partner Agent Avatar
                    xlString = (string)(xlrange.Cells[xlRowCnt, 11] as Excel.Range).Value2;
                    int num = agents_PartnerAgentsHelper.XpathCount("//div[@id='user_data']/div/div[3]/ul/li");
                    for (int i = 1; i <= num; i++)
                    {
                        string name = agents_PartnerAgentsHelper.GetText("//div[@id='user_data']/div/div[3]/ul/li[" + i + "]/label/span");
                        if (name == xlString)
                        {
                            agents_PartnerAgentsHelper.Click("//div[@id='user_data']/div/div[3]/ul/li[" + i + "]/input");
                            break;
                        }
                        else
                            continue;
                    }

                    //Click on Save
                    agents_PartnerAgentsHelper.ClickElement("ClickSave");
                    agents_PartnerAgentsHelper.WaitForWorkAround(3000);
                    if (GetWebDriver().PageSource.Contains("This username already taken") == true)
                        continue;
                }

                else if (xlString == "Referral Association")
                {
                    GetWebDriver().Navigate().GoToUrl("https://www.mypegasuscrm.com/newthemecorp/" + username[0] + "/partners/association/create");
                    agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                    //Partner Association First Name
                    xlString = (string)(xlrange.Cells[xlRowCnt, 2] as Excel.Range).Value2;
                    agents_PartnerAssociationHelper.TypeText("FirstNAME", xlString);

                    //Partner Association Last Name
                    xlString = (string)(xlrange.Cells[xlRowCnt, 3] as Excel.Range).Value2;
                    agents_PartnerAssociationHelper.TypeText("LastName", xlString);

                    //Partner Association Birthday
                    xlString = (string)(xlrange.Cells[xlRowCnt, 4] as Excel.Range).Value2;
                    agents_PartnerAssociationHelper.TypeText("Birthday", xlString);

                    //Partner Association Name
                    xlString = (string)(xlrange.Cells[xlRowCnt, 5] as Excel.Range).Value2;
                    agents_PartnerAssociationHelper.TypeText("Name", xlString);

                    //Partner Association eAddress Type
                    xlString = (string)(xlrange.Cells[xlRowCnt, 6] as Excel.Range).Value2;
                    agents_PartnerAssociationHelper.SelectByText("eAddressType", xlString);
                    agents_PartnerAssociationHelper.WaitForWorkAround(1000);

                    //Partner Association eAddress Label
                    xlString = (string)(xlrange.Cells[xlRowCnt, 7] as Excel.Range).Value2;
                    agents_PartnerAssociationHelper.SelectByText("eAddressLebel", xlString);

                    //Partner Association eAddress
                    xlString = (string)(xlrange.Cells[xlRowCnt, 8] as Excel.Range).Value2;
                    agents_PartnerAssociationHelper.TypeText("eAddress", xlString);

                    //Partner Association Username
                    xlString = (string)(xlrange.Cells[xlRowCnt, 9] as Excel.Range).Value2;
                    agents_PartnerAssociationHelper.TypeText("UserName", xlString);

                    //Partner Association Password
                    agents_PartnerAssociationHelper.ClickElement("AutoGenPswd");
                    agents_PartnerAssociationHelper.WaitForWorkAround(1000);
                    try
                    {
                        xlDouble = (double)(xlrange.Cells[xlRowCnt, 10] as Excel.Range).Value2;
                        xlString = "" + xlDouble;
                        agents_PartnerAssociationHelper.TypeText("Password", xlString);
                    }
                    catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                    {
                        xlString = (string)(xlrange.Cells[xlRowCnt, 10] as Excel.Range).Value2;
                        agents_PartnerAssociationHelper.TypeText("Password", xlString);
                    }

                    //Partner Association Avatar
                    xlString = (string)(xlrange.Cells[xlRowCnt, 11] as Excel.Range).Value2;
                    int num = agents_PartnerAssociationHelper.XpathCount("//form[@id='PartnerAssociationAssociationCreateForm']/div[3]/div[3]/div/div/div[2]/div/div[3]/ul/li");
                    for (int i = 1; i <= num; i++)
                    {
                        string name = agents_PartnerAssociationHelper.GetText("//form[@id='PartnerAssociationAssociationCreateForm']/div[3]/div[3]/div/div/div[2]/div/div[3]/ul/li[" + i + "]/label/span");
                        if (name == xlString)
                        {
                            agents_PartnerAssociationHelper.Click("//form[@id='PartnerAssociationAssociationCreateForm']/div[3]/div[3]/div/div/div[2]/div/div[3]/ul/li[" + i + "]/input");
                            break;
                        }
                        else
                            continue;
                    }

                    //Click on Save
                    agents_PartnerAssociationHelper.ClickElement("ClickSaveBTN");
                    agents_PartnerAssociationHelper.WaitForWorkAround(3000);
                    if (GetWebDriver().PageSource.Contains("This username already taken") == true)
                        continue;
                }
                else
                    continue;
            }


        }
    }
}

