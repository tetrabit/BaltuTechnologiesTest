// Copyright 2019 The Gamedev Guru (http://thegamedev.guru)
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#pragma warning disable 4014
using Amazon.S3;
using Amazon.S3.Transfer;
using System;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using UnityEditor;
using UnityEngine;

namespace TheGamedevGuru
{
    /// <summary>
    /// This script will help you uploading your Unity Addressables content to your S3 Bucket.
    /// You'll need to set up some tokens in the IAM console: https://console.aws.amazon.com/iam/home?#/home
    /// There's a quick tutorial on that here: https://aws.amazon.com/premiumsupport/knowledge-center/create-access-key/
    /// lastly, to find your bucket region name, visit the following table: https://docs.aws.amazon.com/general/latest/gr/rande.html#s3_region
    /// Questions? E-mail me at ruben@thegamedev.guru
    /// </summary>
    public class UploadToS3 : EditorWindow
    {
        private string region;
        private string bucketName;
        private string iamAccessKeyId;
        private string iamSecretKey;
        private RegionEndpoint bucketRegion;
        private const string ServerDataPath = "Assets/StreamingAssets/ServerData";

        [MenuItem("Tools/Addressable Content Uploader (S3)")]
        static void Init()
        {
            var window = (UploadToS3)EditorWindow.GetWindow(typeof(UploadToS3));
            window.region = EditorPrefs.GetString("UploadToS3_region", "eu-central-1");
            window.bucketName = EditorPrefs.GetString("UploadToS3_bucketName", "thegamedevguru");
            window.iamAccessKeyId = EditorPrefs.GetString("UploadToS3_iamAccessKeyId", "** SET ACCESS KEY ID **");
            window.iamSecretKey = EditorPrefs.GetString("UploadToS3_iamSecretKey", "** SET SECRET KEY **");
            window.Show();
        }
        
        void OnGUI()
        {
            GUILayout.Label("Base Settings", EditorStyles.boldLabel);
            region = EditorGUILayout.TextField("Region", region);
            bucketName = EditorGUILayout.TextField("Bucket name", bucketName);
            iamAccessKeyId = EditorGUILayout.TextField("IAM Access Key Id", iamAccessKeyId);
            iamSecretKey = EditorGUILayout.TextField("IAM Secret Key", iamSecretKey);
            if (GUILayout.Button("Upload content"))
            {
                EditorPrefs.SetString("UploadToS3_region", region);
                EditorPrefs.SetString("UploadToS3_bucketName", bucketName);
                EditorPrefs.SetString("UploadToS3_iamAccessKeyId", iamAccessKeyId);
                EditorPrefs.SetString("UploadToS3_iamSecretKey", iamSecretKey);
                bucketRegion = RegionEndpoint.GetBySystemName(region);
                InitiateTask();
            }

            if (GUILayout.Button("Help me setting up the bucket!"))
            {
                Application.OpenURL("https://thegamedev.guru/unity-addressables/hosting-with-amazon-s3/");
            }
            if (GUILayout.Button("Help me finding my region!"))
            {
                Application.OpenURL("https://docs.aws.amazon.com/general/latest/gr/rande.html#s3_region");
            }
            if (GUILayout.Button("Help me setting up the keys!"))
            {
                Application.OpenURL("https://aws.amazon.com/premiumsupport/knowledge-center/create-access-key/");
            }
            if (GUILayout.Button("I still need help!"))
            {
                EditorUtility.DisplayDialog("Help", "Ok, ok, send me an e-mail to ruben@thegamedev.guru and I'll see what I can do. Otherwise, post a comment in the blog article", "Ok thanks");
            }
        }
        
        async Task InitiateTask()   {
            await UploadAsync(bucketRegion, bucketName, iamAccessKeyId, iamSecretKey, "ServerData");
        }
        
        private static async Task UploadAsync(RegionEndpoint bucketRegion, string bucketName, string iamAccessKeyId, string iamSecretKey, string path)
        {
            try
            {
                Debug.Log("Starting upload...");
                var credentials = new BasicAWSCredentials(iamAccessKeyId, iamSecretKey);
                var s3Client = new AmazonS3Client(credentials,  bucketRegion);

                var transferUtility = new TransferUtility(s3Client);

                var transferUtilityRequest = new TransferUtilityUploadDirectoryRequest
                {
                    BucketName = bucketName,
                    Directory = path,
                    StorageClass = S3StorageClass.StandardInfrequentAccess,
                    CannedACL = S3CannedACL.PublicRead,
                    SearchOption = SearchOption.AllDirectories,
                    
                };
                await transferUtility.UploadDirectoryAsync(transferUtilityRequest);
                Debug.Log("Upload completed");
            }
            catch (AmazonS3Exception e)
            {
                Debug.LogError("Error encountered on server when writing an object: " + e.Message);
            }
            catch (Exception e)
            {
                Debug.LogError("Unknown encountered on server when writing an object: " + e.Message);
            }

        }
 
    }
}
#pragma warning restore 4014
