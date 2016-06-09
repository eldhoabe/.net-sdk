﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB
{
    public class CloudQuery
    {
        Dictionary<string, Object> dictionary = new Dictionary<string, Object>();
        public CloudQuery(string tableName)
        { //constructor for the class CloudQueryfd
            dictionary["tableName"] = tableName;
            dictionary["query"] = new Dictionary<string, Object>();
            dictionary["$include"] = new ArrayList();
            dictionary["select"] = new Dictionary<string, Object>();
            dictionary["sort"] = new Dictionary<string, Object>();
            dictionary["skip"] = 0;
            dictionary["limit"] = 20; //limit to 20 documents by default.
        }
        // Logical operations
        public static CloudQuery Or(CloudQuery query1, CloudQuery query2)
        {
            if (query1.dictionary["tableName"].ToString()!= query2.dictionary["tableName"].ToString())
            {
                throw new Exception.CloudBoostException("Tablename of two query objects are not the same.");
            }

            var query = new CloudQuery((string)query1.dictionary["tableName"]);
            ArrayList list = new ArrayList();
            list.Add(query1);
            list.Add(query2);
            ((Dictionary<string, Object>)(query.dictionary["query"]))["$or"] = list;

            return query;
        }


        public CloudQuery EqualTo(string columnName, Object data)
        {
            if (columnName == "ID" || columnName == "id")
            {
                columnName = "_id";
            }
            ((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] = data;

            return this;
        }

        public CloudQuery Include(string columnName)
        {
            if (columnName == "ID")
            {
                columnName = "_id";
            }
            ((ArrayList)((Dictionary<string, Object>)(this.dictionary["query"]))["$include"]).Add(columnName);

            return this;
        }

        public CloudQuery NotEqualTo(string columnName, Object data)
        {
            if (columnName == "ID")
            {
                columnName = "_id";
            }

            if (((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] == null || ((Dictionary<string, Object>)(this.dictionary["query"]))[columnName].GetType() == typeof(Dictionary<string, Object>))
            {
                ((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] = new Dictionary<string, Object>();
            }

            ((Dictionary<string, Object>)(((Dictionary<string, Object>)(this.dictionary["query"]))[columnName]))["$ne"] = data;

            return this;
        }

        public CloudQuery GreaterThan(string columnName, Object data)
        {
            if (columnName == "ID")
            {
                columnName = "_id";
            }

            if (((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] == null || ((Dictionary<string, Object>)(this.dictionary["query"]))[columnName].GetType() == typeof(Dictionary<string, Object>))
            {
                ((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] = new Dictionary<string, Object>();
            }

            ((Dictionary<string, Object>)(((Dictionary<string, Object>)(this.dictionary["query"]))[columnName]))["$gt"] = data;

            return this;
        }
        public CloudQuery GreaterThanEqualTo(string columnName, Object data)
        {
            if (columnName == "ID")
            {
                columnName = "_id";
            }

            if (((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] == null || ((Dictionary<string, Object>)(this.dictionary["query"]))[columnName].GetType() == typeof(Dictionary<string, Object>))
            {
                ((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] = new Dictionary<string, Object>();
            }

            ((Dictionary<string, Object>)(((Dictionary<string, Object>)(this.dictionary["query"]))[columnName]))["$gte"] = data;

            return this;
        }
        public CloudQuery LessThan(string columnName, Object data)
        {
            if (columnName == "ID")
            {
                columnName = "_id";
            }

            if (((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] == null || ((Dictionary<string, Object>)(this.dictionary["query"]))[columnName].GetType() == typeof(Dictionary<string, Object>))
            {
                ((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] = new Dictionary<string, Object>();
            }

            ((Dictionary<string, Object>)(this.dictionary[columnName]))["$lt"] = data;

            return this;
        }
        public CloudQuery LessThanEqualTo(string columnName, Object data)
        {
            if (columnName == "ID")
            {
                columnName = "_id";
            }

            if (((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] == null || ((Dictionary<string, Object>)(this.dictionary["query"]))[columnName].GetType() == typeof(Dictionary<string, Object>))
            {
                ((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] = new Dictionary<string, Object>();
            }

            ((Dictionary<string, Object>)(((Dictionary<string, Object>)(this.dictionary["query"]))[columnName]))["$lte"] = data;

            return this;
        }


        //Sorting
        public CloudQuery OrderByAsc(string columnName)
        {
            if (columnName == "ID")
            {
                columnName = "_id";
            }
           ((Dictionary<string, Object>)(this.dictionary["sort"]))[columnName] = 1;

            return this;
        }
        public CloudQuery OrderByDesc(string columnName)
        {
            if (columnName == "ID")
            {
                columnName = "_id";
            }
           ((Dictionary<string, Object>)(this.dictionary["sort"]))[columnName] = -1;

            return this;
        }

        public int Limit
        {
            get
            {
                return (int)dictionary["limit"];
            }
            set
            {
                dictionary["limit"] = value;
            }
        }

        public Dictionary<string, Object> Query
        {
            get
            {
                return (Dictionary<string, Object>)dictionary["query"];
            }
            set
            {
                dictionary["query"] = value;
            }
        }

        public Dictionary<string, Object> Sort
        {
            get
            {
                return (Dictionary<string, Object>)dictionary["sort"];
            }
            set
            {
                dictionary["sort"] = value;
            }
        }

        public int Skip
        {
            get
            {
                return (int)dictionary["skip"];
            }
            set
            {
                dictionary["skip"] = value;
            }
        }
        
        //select/deselect columns to show
        public CloudQuery SelectColumn(string columnName)
        {
            if (columnName == "ID")
            {
                columnName = "_id";
            }

            ((Dictionary<string, Object>)(this.dictionary["select"]))[columnName] = 1;

            return this;
        }

        public CloudQuery SelectColumn(string[] columnNames)
        {
            for (int i = 0; i < columnNames.Length; i++)
            {
                if (columnNames[i] == "ID")
                {
                    columnNames[i] = "_id";
                }

                ((Dictionary<string, Object>)(this.dictionary["select"]))[columnNames[i]] = 1;
            }
            

            return this;
        }

        public CloudQuery DoNotSelectColumn(string columnName)
        {
            if (columnName == "ID")
            {
                columnName = "_id";
            }

            ((Dictionary<string, Object>)(this.dictionary["select"]))[columnName] = 0;

            return this;
        }


        public CloudQuery DoNotSelectColumn(string[] columnNames)
        {
            for (int i = 0; i < columnNames.Length; i++)
            {
                if (columnNames[i] == "ID")
                {
                    columnNames[i] = "_id";
                }

                ((Dictionary<string, Object>)(this.dictionary["select"]))[columnNames[i]] = 0;
            }

            return this;
        }

        public CloudQuery ContainedIn(string columnName, object data)
        {
            if (columnName == "ID")
            {
                columnName = "_id";
            }

            if (((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] == null || ((Dictionary<string, Object>)(this.dictionary["query"]))[columnName].GetType() == typeof(Dictionary<string, Object>))
            {
                ((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] = new Dictionary<string, Object>();
            }

            if(((Dictionary<string, Object>)((Dictionary<string, Object>)(this.dictionary["query"]))[columnName])["$in"] == null)
            {
                ((Dictionary<string, Object>)((Dictionary<string, Object>)(this.dictionary["query"]))[columnName])["$in"] = new ArrayList();
            }

            ((ArrayList)((Dictionary<string, Object>)((Dictionary<string, Object>)(this.dictionary["query"]))[columnName])["$in"]).Add(data);

            if (((Dictionary<string, Object>)((Dictionary<string, Object>)(this.dictionary["query"]))[columnName]) != null && ((ArrayList)((Dictionary<string, Object>)((Dictionary<string, Object>)(this.dictionary["query"]))[columnName])["$nin"]) != null && ((ArrayList)((Dictionary<string, Object>)((Dictionary<string, Object>)(this.dictionary["query"]))[columnName])["$nin"]).GetType() == typeof(ArrayList))
            {
                if (((ArrayList)((Dictionary<string, Object>)((Dictionary<string, Object>)(this.dictionary["query"]))[columnName])["$nin"]).IndexOf(data) > -1)
                {
                    //remove from not contained in list
                    ((ArrayList)((Dictionary<string, Object>)((Dictionary<string, Object>)(this.dictionary["query"]))[columnName])["$nin"]).Remove(data);
                }
            }

            return this;
        }

        public CloudQuery ContainedIn(string columnName, string[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                ContainedIn(columnName, data[i]);
            }

            return this;
        }

        public CloudQuery NotContainedIn(string columnName, string data)
        {
            if (columnName == "ID")
            {
                columnName = "_id";
            }

            if (((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] == null || ((Dictionary<string, Object>)(this.dictionary["query"]))[columnName].GetType() == typeof(Dictionary<string, Object>))
            {
                ((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] = new Dictionary<string, Object>();
            }

            if (((Dictionary<string, Object>)((Dictionary<string, Object>)(this.dictionary["query"]))[columnName])["$nin"] == null)
            {
                ((Dictionary<string, Object>)((Dictionary<string, Object>)(this.dictionary["query"]))[columnName])["$nin"] = new ArrayList();
            }

            ((ArrayList)((Dictionary<string, Object>)((Dictionary<string, Object>)(this.dictionary["query"]))[columnName])["$nin"]).Add(data);

            if (((Dictionary<string, Object>)((Dictionary<string, Object>)(this.dictionary["query"]))[columnName]) != null && ((ArrayList)((Dictionary<string, Object>)((Dictionary<string, Object>)(this.dictionary["query"]))[columnName])["$in"]) != null && ((ArrayList)((Dictionary<string, Object>)((Dictionary<string, Object>)(this.dictionary["query"]))[columnName])["$in"]).GetType() == typeof(ArrayList))
            {
                if (((ArrayList)((Dictionary<string, Object>)((Dictionary<string, Object>)(this.dictionary["query"]))[columnName])["$in"]).IndexOf(data) > -1)
                {
                    //remove from not contained in list
                    ((ArrayList)((Dictionary<string, Object>)((Dictionary<string, Object>)(this.dictionary["query"]))[columnName])["$in"]).Remove(data);
                }
            }

            return this;
        }

        public CloudQuery NotContainedIn(string columnName, string[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                NotContainedIn(columnName, data[i]);
            }

            return this;
        }


        public CloudQuery Exists(string columnName)
        {
            if (columnName == "ID")
            {
                columnName = "_id";
            }

            if (((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] == null || ((Dictionary<string, Object>)(this.dictionary["query"]))[columnName].GetType() == typeof(Dictionary<string, Object>))
            {
                ((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] = new Dictionary<string, Object>();
            }

           ((Dictionary<string, Object>)(((Dictionary<string, Object>)(this.dictionary["query"]))[columnName]))["$exists"] = true;

            return this;
        }

        public CloudQuery DoesNotExist(string columnName)
        {
            if (columnName == "ID")
            {
                columnName = "_id";
            }

            if (((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] == null || ((Dictionary<string, Object>)(this.dictionary["query"]))[columnName].GetType() == typeof(Dictionary<string, Object>))
            {
                ((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] = new Dictionary<string, Object>();
            }

           ((Dictionary<string, Object>)(((Dictionary<string, Object>)(this.dictionary["query"]))[columnName]))["$exists"] = false;

            return this;
        }

        public CloudQuery ContainsAll(string columnName, Object[] values)
        {
            if (columnName == "ID")
            {
                columnName = "_id";
            }

            if (((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] == null || ((Dictionary<string, Object>)(this.dictionary["query"]))[columnName].GetType() == typeof(Dictionary<string, Object>))
            {
                ((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] = new Dictionary<string, Object>();
            }

           (((Dictionary<string, Object>)((Dictionary<string, Object>)(this.dictionary["query"]))[columnName]))["$all"] = values;

            return this;
        }

        public CloudQuery StartsWith(string columnName, string value)
        {
            var regex = '^' + value;

            if (((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] == null || ((Dictionary<string, Object>)(this.dictionary["query"]))[columnName].GetType() == typeof(Dictionary<string, Object>))
            {
                ((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] = new Dictionary<string, Object>();
            }

            if (((Dictionary<string, Object>)(this.dictionary["query"])) != null)
            {
                ((Dictionary<string, Object>)(this.dictionary["query"]))["$regex"] = regex;
                ((Dictionary<string, Object>)(this.dictionary["query"]))["$options"] = "im";
            }

            return this;
        }

        public CloudQuery RegEx(string columnName, string value)
        {
            if (columnName.ToLower() == "id")
            {
                columnName = "_" + columnName;
            }

            if (((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] == null || ((Dictionary<string, Object>)(this.dictionary["query"]))[columnName].GetType() == typeof(Dictionary<string, Object>))
            {
                ((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] = new Dictionary<string, Object>();
            }

            if (((Dictionary<string, Object>)(this.dictionary["query"])) != null)
            {
                ((Dictionary<string, Object>)(this.dictionary["query"]))["$regex"] = value;
            }

            return this;
        }

        public CloudQuery SubString(string columnName, ArrayList value)
        {
            var list = new ArrayList();
            list.Add(columnName);
            return SubString(list, value);
        }

        public CloudQuery SubString(ArrayList columnName, ArrayList value)
        {
            for (int i = 0; i < columnName.Count; i++)
            {
                if (((Dictionary<string, Object>)(this.dictionary["query"]))["$or"] == null || ((Dictionary<string, Object>)(this.dictionary["query"]))["$or"].GetType() == typeof(ArrayList))
                {
                    ((Dictionary<string, Object>)(this.dictionary["query"]))["$or"] = new ArrayList();
                }

                for (int j = 0; j < value.Count; j++)
                {
                    var obj = new Dictionary<string, Object>();
                    obj[columnName[i].ToString()] = new Dictionary<string, Object>();
                    ((Dictionary<string, Object>)obj[columnName[i].ToString()])["$regex"] = ".*" + value[j] + ".*";
                    ((ArrayList)((Dictionary<string, Object>)(this.dictionary["query"]))["$or"]).Add(obj);
                }
            }
            return this;
        }

        //GeoPoint near query
        public CloudQuery Near(string columnName, CB.CloudGeoPoint geoPoint, double maxDistance, double minDistance)
        {
            if (((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] == null)
            {
                ((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] = new Dictionary<string, Object>();
                Dictionary<string, object> near = new Dictionary<string,object>();
                Dictionary<string, object> geometry = new Dictionary<string,object>();
                double[] coordinates = (double[])geoPoint.dictionary["coordinates"];
                geometry.Add("coordinates", coordinates);
                geometry.Add("type", "Point");
                near.Add("$geometry", geometry);
                near.Add("$maxDistance", maxDistance);
                near.Add("$minDistance", minDistance);
                ((Dictionary<string, Object>)(((Dictionary<string, Object>)(this.dictionary["query"]))[columnName]))["$near"] = near;
            }
            return this;
        }

        public CloudQuery Near(string columnName, CB.CloudGeoPoint geoPoint, double maxDistance)
        {
            if (((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] == null)
            {
                ((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] = new Dictionary<string, Object>();
                Dictionary<string, object> near = new Dictionary<string,object>();
                Dictionary<string, object> geometry = new Dictionary<string,object>();
                double[] coordinates = (double[])geoPoint.dictionary["coordinates"];
                geometry.Add("coordinates", coordinates);
                geometry.Add("type", "Point");
                near.Add("$geometry", geometry);
                near.Add("$maxDistance", maxDistance);
                near.Add("$minDistance", null);
                ((Dictionary<string, Object>)(((Dictionary<string, Object>)(this.dictionary["query"]))[columnName]))["$near"] = near;
            }
            return this;
        }

        //GeoPoint geoWithin query
        public CloudQuery GeoWithin(string columnName, CloudGeoPoint[] geoPoint)
        {
            double[][] coordinates = {};
            for(int i=0; i<geoPoint.Length; i++)
            {
                coordinates[i] = (double[])geoPoint[i].dictionary["coordinates"];
            }
            string type = "Polygon";
            if (((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] == null)
            {
                ((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] = new Dictionary<string, Object>();
                Dictionary<string, object> geoWithin = new Dictionary<string,object>();
                Dictionary<string, object> geometry = new Dictionary<string,object>();
                geometry.Add("coordinates", coordinates);
                geometry.Add("type", type);
                geoWithin.Add("$geometry", geometry);
                ((Dictionary<string, Object>)(((Dictionary<string, Object>)(this.dictionary["query"]))[columnName]))["$geoWithin"] = geoWithin;
            }

            return this;
        }

        public CloudQuery GeoWithin(string columnName, CloudGeoPoint geoPoint, double radius)
        {
            double[] coordinates = (double[])geoPoint.dictionary["coordinates"];
            
            if (((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] == null)
            {
                ((Dictionary<string, Object>)(this.dictionary["query"]))[columnName] = new Dictionary<string, Object>();
                Dictionary<string, object> geoWithin = new Dictionary<string,object>();
                Dictionary<string, object> centerSphere = new Dictionary<string,object>();
                centerSphere.Add("coordinates", coordinates);
                centerSphere.Add("radius", radius/3963.2);
                geoWithin.Add("$geometry", centerSphere);
                ((Dictionary<string, Object>)(((Dictionary<string, Object>)(this.dictionary["query"]))[columnName]))["$geoWithin"] = geoWithin;
            }

            return this;
        }

        public async Task<int> CountAsync()
        {
            var postData = new Dictionary<string, Object>();
            postData["query"] = this.dictionary["query"];
            postData["limit"] = this.dictionary["limit"];
            postData["skip"] = this.dictionary["skip"];
            var result = await Util.CloudRequest.Send<object>(Util.CloudRequest.Method.POST, CloudApp.ApiUrl + "/data/" + CloudApp.AppID + "/" + this.dictionary["tableName"] + "/count", this.dictionary);
            return (int)result;
        }

        public async Task<List<CB.CloudObject>> DistinctAsync(string key)
        {
            var postData = new Dictionary<string, Object>();
            postData["onKey"] = key;
            postData["query"] = this.dictionary["query"];
            postData["select"] = this.dictionary["select"];
            postData["sort"] = this.dictionary["sort"];
            postData["limit"] = this.dictionary["limit"];
            postData["skip"] = this.dictionary["skip"];
            var result = await Util.CloudRequest.Send<List<Dictionary<string, Object>>>(Util.CloudRequest.Method.POST, CloudApp.ApiUrl + "/data/" + CloudApp.AppID + "/" + this.dictionary["tableName"] + "/distinct", postData);
            List<CloudObject> list = CB.PrivateMethods.ToCloudObjectList(result);
            return list;
        }

        public async Task<List<CB.CloudObject>> FindAsync()
        {
            var postData = new Dictionary<string, Object>();
            postData["query"] = this.dictionary["query"];
            postData["select"] = this.dictionary["select"];
            postData["sort"] = this.dictionary["sort"];
            postData["limit"] = this.dictionary["limit"];
            postData["skip"] = this.dictionary["skip"];

            var result = await Util.CloudRequest.Send<List<Dictionary<string, Object>>>(Util.CloudRequest.Method.POST, CloudApp.ApiUrl + "/data/" + CloudApp.AppID + "/" + this.dictionary["tableName"] + "/find", postData);
            List<CloudObject> list = CB.PrivateMethods.ToCloudObjectList(result);
            return list;
        }

        public async Task<Dictionary<string, Object>> PaginateAsync(int pageNo, int totalItemsInPage)
        {
            if (pageNo > 0)
            {
                if (totalItemsInPage > 0)
                {
                    int skip = (pageNo * totalItemsInPage) - totalItemsInPage;
                    this.Skip = skip;
                    this.Limit = totalItemsInPage;
                }


            }

            if (totalItemsInPage > 0)
            {
                this.Limit = totalItemsInPage;
            }

            var findTask = Util.CloudRequest.Send<List<Dictionary<string, Object>>>(Util.CloudRequest.Method.POST, CloudApp.ApiUrl + "/data/" + CloudApp.AppID + "/ " + this.dictionary["tableName"] + "/find", this.dictionary);
            var countObj = new CB.CloudQuery(this.dictionary["tableName"].ToString());
            countObj.dictionary = this.dictionary;
            var countTask = Util.CloudRequest.Send <object> (Util.CloudRequest.Method.POST,  CloudApp.ApiUrl + "/data/" + CloudApp.AppID + "/" + this.dictionary["tableName"] + "/count", countObj.dictionary);
            await Task.WhenAll(findTask, countTask);
            var findResult = await findTask;
            List<CloudObject> list = CB.PrivateMethods.ToCloudObjectList(findResult);
            var countResult = await countTask;
            int count = (int)countResult;
            int totalPages = 0;

            if (countResult != null)
            {
                count = 0;
            }
            else
            {
                totalPages = (int)(count / this.Limit);
            }
            
            if (totalPages < 0)
            {
                totalPages = 0;
            }

            var resultObject = new Dictionary<string, Object>();
            resultObject.Add("objectsList", findResult);
            resultObject.Add("count", count);
            resultObject.Add("totalPages", totalPages);
            return resultObject;
        }

        public async Task<CB.CloudObject> GetAsync(string objectId)
        {
            this.EqualTo("id", objectId);
            CB.CloudObject obj = await this.FindOneAsync();
            return obj;
        }

        public async Task<CloudObject> FindOneAsync()
        {
            var postData = new Dictionary<string, Object>();
            postData["query"] = this.dictionary["query"];
            postData["select"] = this.dictionary["select"];
            postData["sort"] = this.dictionary["sort"];
            postData["skip"] = this.dictionary["skip"];
            var result = await Util.CloudRequest.Send<Dictionary<string, Object>>(Util.CloudRequest.Method.POST, CloudApp.ApiUrl + "/data/" + CloudApp.AppID + "/" + this.dictionary["tableName"] + "/findOne" , postData);

            if (result == null)
                return null;

            var obj = new CloudObject(result["_tableName"].ToString());
            obj.dictionary = result;
            return obj;
        }
    }
}
