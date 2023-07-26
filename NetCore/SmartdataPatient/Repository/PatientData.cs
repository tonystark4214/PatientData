using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SmartdataPatient.Models;
using System.Data;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace SmartdataPatient.Repository
{
    public class PatientData:IPatientData
    {
        private sdirectdbContext dbContext;

        public PatientData(sdirectdbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        //getdata api
        public ResponseMessageModel GetAllPatientData()
        {
            try
            {
                ResponseMessageModel responseMessageModel = new ResponseMessageModel();

                var data = (from p in dbContext.SmartdataPatientData
                            join c in dbContext.SmartdataCountryData on p.CountryId equals c.CountryId
                            where p.IsDeleted == false orderby p.CreatedOn descending
                            select new PatientDataModel
                            {
                                Id=p.PatientId,
                                Name = p.FirstName + " " + p.LastName,
                                Email = p.Email,
                                DOB = p.Dob,
                                CountryName = c.CountryName,
                                PhoneNumber = p.PhoneNumber,
                                UserName = p.UserName
                            }).ToList();

                if (data != null)
                {
                    responseMessageModel.AllPatientData = data;
                    responseMessageModel.Message = "Succesfull";
                    responseMessageModel.Code = 200;
                    return responseMessageModel;
                }
                else
                {
                    responseMessageModel.Message = "Failed";
                    responseMessageModel.Code = 400;
                    return responseMessageModel;
                }
            }
            catch (Exception ex)
            {
                ResponseMessageModel responseMessageModel = new ResponseMessageModel();
                responseMessageModel.Message= ex.Message;
                return responseMessageModel;
            }
        }

        //post and update api
        public ResponseMessageModel PostPatientData(PostModel model)
        {
            try
            {
                var builder = WebApplication.CreateBuilder();
                string conStr = builder.Configuration.GetConnectionString("con");
                ResponseMessageModel responseMessageModel = new ResponseMessageModel();
                SqlConnection con = new SqlConnection(conStr);
                con.Open();
                if (con.State == ConnectionState.Closed)
                    con.Open();
                if (model.id == 0)
                {
                    SqlCommand cmd = new SqlCommand("PostPatientData", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@firstName", SqlDbType.VarChar).Value = model.FirstName;
                    cmd.Parameters.Add("@lastName", SqlDbType.VarChar).Value = model.LastName;
                    cmd.Parameters.Add("@userName", SqlDbType.VarChar).Value = model.UserName;
                    cmd.Parameters.Add("@phoneNumber", SqlDbType.BigInt).Value = model.PhoneNumber;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = model.Email;
                    cmd.Parameters.Add("@dob", SqlDbType.DateTime).Value = model.DOB;
                    cmd.Parameters.Add("@country", SqlDbType.Int).Value = model.Country;
                    cmd.ExecuteNonQuery();

                    responseMessageModel.Message = "Patient Added";
                    responseMessageModel.Code = 200;
                    return responseMessageModel;
                }
                else if (model.id > 0)
                {
                    var data = dbContext.SmartdataPatientData.Where(i => i.PatientId == model.id).FirstOrDefault();
                    if (data != null)
                    {
                        SqlCommand cmd = new SqlCommand("UpdatePatientData", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = model.id;
                        cmd.Parameters.Add("@firstName", SqlDbType.VarChar).Value = model.FirstName;
                        cmd.Parameters.Add("@lastName", SqlDbType.VarChar).Value = model.LastName;
                        cmd.Parameters.Add("@userName", SqlDbType.VarChar).Value = model.UserName;
                        cmd.Parameters.Add("@phoneNumber", SqlDbType.BigInt).Value = model.PhoneNumber;
                        cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = model.Email;
                        cmd.Parameters.Add("@dob", SqlDbType.DateTime).Value = model.DOB;
                        cmd.Parameters.Add("@country", SqlDbType.Int).Value = model.Country;
                        cmd.ExecuteNonQuery();

                        responseMessageModel.Message = "Data Updated";
                        responseMessageModel.Code = 200;
                        return responseMessageModel;

                    }
                    else
                    {
                        responseMessageModel.Message = "nothing to update";
                        responseMessageModel.Code = 400;
                        return responseMessageModel;
                    }
                }
                else
                {
                    responseMessageModel.Message = "cannot be added or updated";
                    responseMessageModel.Code = 400;
                    return responseMessageModel;
                }
            }
            catch(Exception ex) 
            {
                ResponseMessageModel responseMessageModel = new ResponseMessageModel();
                responseMessageModel.Message=ex.Message;
                return responseMessageModel;
            }
        }


        //delete api

        public ResponseMessageModel DeletePatientData(int id, string userName)
        {
            try
            {
                ResponseMessageModel responseMessageModel = new ResponseMessageModel();
                var data=dbContext.SmartdataPatientData.Where(p=>p.PatientId== id).FirstOrDefault();
                if (data!=null)
                {
                    
                    data.IsDeleted = true;
                    data.DeletedOn= DateTime.Now;
                    data.DeletedBy = userName;
                    dbContext.SaveChanges();
                    responseMessageModel.Message = "User Deleted";
                    responseMessageModel.Code = 200;
                    return responseMessageModel;
                }
                else
                {
                    responseMessageModel.Message = "Not Found";
                    responseMessageModel.Code = 400;
                    return responseMessageModel;
                }
            }
            catch (Exception ex)
            {
                ResponseMessageModel responseMessageModel = new ResponseMessageModel();
                responseMessageModel.Message = ex.Message;
                return responseMessageModel;
            }
        }


        //get by id api

        public ResponseMessageModel GetById(int id)
        {
            try
            {


                ResponseMessageModel responseMessageModel = new ResponseMessageModel();
                var data = (from p in dbContext.SmartdataPatientData
                            join c in dbContext.SmartdataCountryData on p.CountryId equals c.CountryId
                            where p.PatientId == id
                            select new PatientDataModel
                            {
                                Id=p.PatientId,
                                FirstName=p.FirstName,
                                LastName=p.LastName,
                                Name = p.FirstName + " " + p.LastName,
                                Email = p.Email,
                                DOB = p.Dob,
                                PhoneNumber = p.PhoneNumber,
                                UserName = p.UserName,
                                countryID=p.CountryId
                                
                            }).FirstOrDefault();

                if (data != null)
                {
                    responseMessageModel.PatientData = data;
                    responseMessageModel.Message = "Succesfull";
                    responseMessageModel.Code = 200;
                    return responseMessageModel;
                }
                else
                {
                    responseMessageModel.Message = "doesnt exist";
                    responseMessageModel.Code = 400;
                    return responseMessageModel;
                }
            }
            catch(Exception ex)
            {
                ResponseMessageModel responseMessageModel = new ResponseMessageModel();
                responseMessageModel.Message = ex.Message;
                return responseMessageModel;
            }
        }

        //country api

        public ResponseMessageModel Country()
        {
            ResponseMessageModel model = new ResponseMessageModel();
            try
            {
                var data = (from c in dbContext.SmartdataCountryData
                            select new CountryModel
                            {
                                CountryId = c.CountryId,
                                CountryName = c.CountryName
                            }).ToList();

                model.Country = data;
                return model;
            }
            catch(Exception ex)
            {
                model.Message= ex.Message;
                return model;
            }
        }
    }
}
