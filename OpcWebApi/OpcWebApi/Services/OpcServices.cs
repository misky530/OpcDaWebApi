using OPCAutomation;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace OpcWebApi.Services
{
    public static class OpcServices
    {
        public static Dictionary<string, string> GetVal(string itemId, string groupName = "AKE_OPC")
        {
            OPCServer opcServer = new OPCServer();
            try
            {
                opcServer = GetInstance();

                // 添加组
                OPCGroup opcGroup = opcServer.OPCGroups.Add(groupName);

                // 添加需要读取的项
                OPCItems opcItems = opcGroup.OPCItems;
                OPCItem opcItem1 = opcItems.AddItem(itemId, 1);

                // 启动同步读取   
                opcItem1.Read(1, out var itemValue, out var quality, out var timestamp);

                Console.WriteLine($"Item Value: {itemValue}, Quality: {quality}, Timestamp: {timestamp}");

                // return  itemId, itemValue
                return new Dictionary<string, string>
                {
                    { itemId, itemValue.ToString() }
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // 断开与 OPC DA 服务器的连接
                opcServer.Disconnect();
            }

            // return empty dictionary
            return new Dictionary<string, string>();

        }

        private static OPCServer GetInstance()
        {
            string ProgId = ConfigurationManager.AppSettings["Key"];
            string Ip = ConfigurationManager.AppSettings["Ip"];

            if (string.IsNullOrEmpty(ProgId) || string.IsNullOrEmpty(Ip))
            {
                throw new Exception("请检查配置文件中的Key和Ip是否正确");
            }

            // 创建OPC DA客户端实例
            OPCServer opcServer = new OPCServer();

            // 连接到OPC DA服务器
            opcServer.Connect(ProgId, Ip);
            //opcServer.Connect("AKE_OPC_SERVER", "192.168.215.253");

            return opcServer;
        }

        private static void Test()
        {
            // 创建OPC DA客户端实例
            OPCServer opcServer = new OPCServer();

            string itemId = "CJT1882004W.440300B0010391.AccumulateFlux"; // OPC 项的 Item ID
            string groupName = "AKE_OPC";

            // 连接到OPC DA服务器
            opcServer.Connect("AKE_OPC_SERVER", "192.168.215.253");

            // 添加组
            OPCGroup opcGroup = opcServer.OPCGroups.Add(groupName);

            // 添加需要读取的项
            OPCItems opcItems = opcGroup.OPCItems;
            OPCItem opcItem1 = opcItems.AddItem(itemId, 1);

            // 启动同步读取
            object itemValue;
            object quality;
            object timestamp;
            opcItem1.Read(1, out itemValue, out quality, out timestamp);

            Console.WriteLine($"Item Value: {itemValue}, Quality: {quality}, Timestamp: {timestamp}");

            // 断开与 OPC DA 服务器的连接
            opcServer.Disconnect();
        }
    }
}