using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Rasmus.AspitVisitor.Business;
using Rasmus.AspitVisitor.DataAccess.EF;

namespace Rasmus.AspitVisitor.API
{
    public class Server
    {
        /// <summary>
        /// The TcpListener used to receive the clients.
        /// </summary>
        private TcpListener listener { get; }
        /// <summary>
        /// The IP Address of the server.
        /// </summary>
        public IPAddress ServerIpAddress { get; }
        public DataHandler handler = new DataHandler();
        public List<string> validRequests = new List<string> { "CountAllVisits", "CountVisitsByDepartment", "CountPotentialStudents", "CountPotentialStudentsByDepartment", "CalculateAgeSpread", "CalculateAverageVisitorAge", "CountVisitsByDepartmentAndDate", "CountNumberOfAnsweredQuestionaires", "CountNumberOfMunicipalities", "CountNumberOfVisitsByMunicipality" };
        /// <summary>
        /// The constructor initializes the IP Address and the TcpListener.
        /// Uses either local IP Address, or the IPv4 Address of the computer.
        /// </summary>
        /// <param name="port">Which port to be used</param>
        /// <param name="useLocal">Optional, is true by default.</param>
        public Server(int port, bool useLocal = true)
        {
            if (useLocal)
                ServerIpAddress = IPAddress.Parse("127.0.0.1");
            else
            {
                var hostName = Dns.GetHostName();
                var ipAddresses = Dns.GetHostAddresses(hostName);
                ServerIpAddress = ipAddresses.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault();
            }

            if (ServerIpAddress == null)
                throw new NullReferenceException("No IPv4 address for the server");

            listener = new TcpListener(ServerIpAddress, port);
        }

        /// <summary>
        /// This method starts the server listening for a client.
        /// It receives the request, handles it, and returns the response.
        /// It is ran in a infinite loop.
        /// </summary>
        public void Start()
        {
            listener.Start();

            while (true)
            {
                try
                {

                    using (TcpClient client = listener.AcceptTcpClient())
                    using (NetworkStream ns = client.GetStream())
                    using (StreamWriter writer = new StreamWriter(ns))
                    using (StreamReader reader = new StreamReader(ns))
                    {
                        writer.AutoFlush = true;

                        string request = reader.ReadLine();
                        string response = Handle(request);

                        writer.WriteLine(response);
                    }
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        /// Method used to handle the request from the client.
        /// </summary>
        /// <param name="request">The request from the client. Must be CSV and atleast two values.</param>
        /// <returns>Returns the response to the client</returns>
        private string Handle(string request)
        {
            var splittedRequest = request.Split(',');
            bool isValid = VerifyRequest(splittedRequest);
            string response = "";

            if (isValid)
            {

                switch (splittedRequest[0])
                {
                    case "CountAllVisits":
                        response += $"Total number of visits: {handler.Reader.CountAllVisits()}";
                        break;
                    case "CountVisitsByDepartment":
                        response += $"Number of visits at {splittedRequest[1]}: {handler.Reader.CountVisitsByDepartment(new AspitDepartment { departmentName = splittedRequest[1] })}";
                        break;
                    case "CountPotentialStudents":
                        response += $"Number of potential students: {handler.Reader.CountPotentialStudents()}";
                        break;
                    case "CountPotentialStudentsByDepartment":
                        response += $"Number of potential students at {splittedRequest[1]}: {handler.Reader.CountPotentialStudentsByDepartment(new AspitDepartment { departmentName = splittedRequest[1] })}";
                        break;
                    case "CalculateAgeSpread":
                        response += $"Age spread: {handler.Reader.CalculateAgeSpread()}";
                        break;
                    case "CalculateAverageVisitorAge":
                        response += $"Average visitor age: {handler.Reader.CalculateAverageVisitorAge()}";
                        break;
                    case "CountVisitsByDepartmentAndDate":
                        response += $"Number of visits at {splittedRequest[1]} on {splittedRequest[2]}: {handler.Reader.CountVisitsByDepartmentAndDate(new AspitDepartment { departmentName = splittedRequest[1] }, DateTime.Parse(splittedRequest[2]))}";
                        break;
                    case "CountNumberOfAnsweredQuestionaires":
                        response += $"Number of answered questionaires: {handler.Reader.CountNumberOfAnsweredQuestionaires()}";
                        break;
                    case "CountNumberOfMunicipalities":
                        response += $"Number of different municipalities: {handler.Reader.CountNumberOfMunicipalities()}";
                        break;
                    case "CountNumberOfVisitsByMunicipality":
                        response += $"Number of visits from {splittedRequest[1]}: {handler.Reader.CountNumberOfVisitsByMunicipality(splittedRequest[1])}";
                        break;
                    default:
                        response = "error,wrong method call";
                        break;
                }
            }
            else
            {
                response += "Error, request not valid";
            }
            return response;
        }

        /// <summary>
        /// Method used to validate the request.
        /// </summary>
        /// <param name="splittedRequest">The splitted CSV request</param>
        /// <returns>Returns true if valid</returns>
        private bool VerifyRequest(string[] splittedRequest)
        {
            if (splittedRequest[0].GetType() == typeof(string) && validRequests.Contains(splittedRequest[0]))
            {
                if ((splittedRequest[0] == "CountVisitsByDepartment" || splittedRequest[0] == "CountPotentialStudentsByDepartment" || splittedRequest[0] == "CountNumberOfVisitsByMunicipality") && splittedRequest.Length != 2)
                {
                    return false;
                }
                if (splittedRequest[0] == "CountVisitsByDepartmentAndDate" && splittedRequest.Length != 3)
                {
                    return false;
                }
                if (!DateTime.TryParse(splittedRequest[2], out DateTime dateTime))
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}