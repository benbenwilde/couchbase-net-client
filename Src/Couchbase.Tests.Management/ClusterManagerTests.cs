﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Couchbase.Configuration.Client;
using NUnit.Framework;

namespace Couchbase.Tests.Management
{
    [TestFixture]
    [Category("Integration")]
    public class ClusterManagerTests
    {
        private static readonly string SecondaryIp = ConfigurationManager.AppSettings["SecondaryIp"];
        private static readonly string PrimaryIp = ConfigurationManager.AppSettings["serverIp"];

        [Test]
        public void Test_AddNode()
        {
            var configuration = new ClientConfiguration
            {
                Servers = new List<Uri>
                {
                    new Uri(ConfigurationManager.AppSettings["bootstrapUrl"])
                }
            };
            using (var cluster = new Cluster(configuration))
            {
                var clusterManager = cluster.CreateManager("Administrator", "password");
                var result = clusterManager.AddNode(SecondaryIp);
                Assert.IsNullOrEmpty(result.Message);
                Assert.IsTrue(result.Success);
            }
        }


        [Test]
        public void Test_AddNodeAsync()
        {
            if (SecondaryIp.Equals(PrimaryIp))
            {
                Assert.Ignore();
            }
            var configuration = new ClientConfiguration
            {
                Servers = new List<Uri>
                {
                    new Uri(ConfigurationManager.AppSettings["bootstrapUrl"])
                }
            };
            using (var cluster = new Cluster(configuration))
            {
                var clusterManager = cluster.CreateManager("Administrator", "password");
                var result = clusterManager.AddNodeAsync(SecondaryIp).Result;
                Assert.IsNullOrEmpty(result.Message);
                Assert.IsTrue(result.Success);
            }
        }

        [Test]
        public void Test_RemoveNodeAsync()
        {
            if (SecondaryIp.Equals(PrimaryIp))
            {
                Assert.Ignore();
            }
            var configuration = new ClientConfiguration
            {
                Servers = new List<Uri>
                {
                    new Uri(ConfigurationManager.AppSettings["bootstrapUrl"])
                }
            };
            using (var cluster = new Cluster(configuration))
            {
                var clusterManager = cluster.CreateManager("Administrator", "password");
                var result = clusterManager.RemoveNodeAsync(SecondaryIp).Result;
                Assert.IsNullOrEmpty(result.Message);
                Assert.IsTrue(result.Success);
            }
        }

        [Test]
        public void Test_RemoveNode()
        {
            var configuration = new ClientConfiguration
            {
                Servers = new List<Uri>
                {
                    new Uri(ConfigurationManager.AppSettings["bootstrapUrl"])
                }
            };
            using (var cluster = new Cluster(configuration))
            {
                var clusterManager = cluster.CreateManager("Administrator", "password");
                var result = clusterManager.RemoveNode(SecondaryIp);
                Console.WriteLine(result.Message);
                Assert.IsTrue(result.Success);
            }
        }


        [Test]
        public void Test_FailoverNode()
        {
            if (SecondaryIp.Equals(PrimaryIp))
            {
                Assert.Ignore();
            }
            var configuration = new ClientConfiguration
            {
                Servers = new List<Uri>
                {
                    new Uri(ConfigurationManager.AppSettings["bootstrapUrl"])
                }
            };
            using (var cluster = new Cluster(configuration))
            {
                var clusterManager = cluster.CreateManager("Administrator", "password");
                var result = clusterManager.FailoverNode(SecondaryIp);
                Assert.IsNullOrEmpty(result.Message);
                Assert.IsTrue(result.Success);
            }
        }

        [Test]
        public void Test_FailoverNodeAsync()
        {
            if (SecondaryIp.Equals(PrimaryIp))
            {
                Assert.Ignore();
            }
            var configuration = new ClientConfiguration
            {
                Servers = new List<Uri>
                {
                    new Uri(ConfigurationManager.AppSettings["bootstrapUrl"])
                }
            };
            using (var cluster = new Cluster(configuration))
            {
                var clusterManager = cluster.CreateManager("Administrator", "password");
                var result = clusterManager.FailoverNodeAsync(SecondaryIp).Result;
                Assert.IsNullOrEmpty(result.Message);
                Assert.IsTrue(result.Success);
            }
        }

        [Test]
        public void Test_ListBuckets()
        {
            var configuration = new ClientConfiguration
            {
                Servers = new List<Uri>
                {
                    new Uri(ConfigurationManager.AppSettings["bootstrapUrl"])
                }
            };
            using (var cluster = new Cluster(configuration))
            {
                var clusterManager = cluster.CreateManager("Administrator", "password");
                var results = clusterManager.ListBuckets();
                Assert.Greater(results.Value.Count, 0);
            }
        }

        [Test]
        public void Test_ListBucketsAsync()
        {
            var configuration = new ClientConfiguration
            {
                Servers = new List<Uri>
                {
                    new Uri(ConfigurationManager.AppSettings["bootstrapUrl"])
                }
            };
            using (var cluster = new Cluster(configuration))
            {
                var clusterManager = cluster.CreateManager("Administrator", "password");
                var results = clusterManager.ListBucketsAsync().Result;
                Assert.Greater(results.Value.Count, 0);
            }
        }

        [Test]
        public void Test_Rebalance()
        {
            var configuration = new ClientConfiguration
            {
                Servers = new List<Uri>
                {
                    new Uri(ConfigurationManager.AppSettings["bootstrapUrl"])
                }
            };
            using (var cluster = new Cluster(configuration))
            {
                var clusterManager = cluster.CreateManager("Administrator", "password");
                var result = clusterManager.Rebalance();
                Assert.IsNullOrEmpty(result.Message);
                Assert.IsTrue(result.Success);
            }
        }


        [Test]
        public void Test_RebalanceAsync()
        {
            var configuration = new ClientConfiguration
            {
                Servers = new List<Uri>
                {
                    new Uri(ConfigurationManager.AppSettings["bootstrapUrl"])
                }
            };
            using (var cluster = new Cluster(configuration))
            {
                var clusterManager = cluster.CreateManager("Administrator", "password");
                var result = clusterManager.RebalanceAsync().Result;
                Assert.IsTrue(result.Success);
            }
        }

        [Test]
        public void Test_ClusterInfo()
        {
            var configuration = new ClientConfiguration
            {
                Servers = new List<Uri>
                {
                    new Uri(ConfigurationManager.AppSettings["bootstrapUrl"])
                }
            };
            using (var cluster = new Cluster(configuration))
            {
                var clusterManager = cluster.CreateManager("Administrator", "password");
                var result = clusterManager.ClusterInfo();
                Assert.NotNull(result.Success);
                Assert.That(result.Success);
                var info = result.Value;
                Assert.NotNull(info);
                Assert.NotNull(info.Pools());
                Assert.NotNull(info.BucketConfigs());
                Assert.Greater(info.BucketConfigs().Count, 0);
                Assert.NotNull(info.BucketConfigs().ElementAt(0));
            }
        }

        [Test]
        public void Test_ClusterInfoAsync()
        {
            var configuration = new ClientConfiguration
            {
                Servers = new List<Uri>
                {
                    new Uri(ConfigurationManager.AppSettings["bootstrapUrl"])
                }
            };
            using (var cluster = new Cluster(configuration))
            {
                var clusterManager = cluster.CreateManager("Administrator", "password");
                var result = clusterManager.ClusterInfoAsync().Result;

                Assert.NotNull(result.Success);
                Assert.That(result.Success);
                var info = result.Value;
                Assert.NotNull(info);
                Assert.NotNull(info.Pools());
                Assert.NotNull(info.BucketConfigs());
                Assert.Greater(info.BucketConfigs().Count, 0);
                Assert.NotNull(info.BucketConfigs().ElementAt(0));
            }
        }
        [Test]
        public void Test_CreateBucket()
        {
            var configuration = new ClientConfiguration
            {
                Servers = new List<Uri>
                {
                    new Uri(ConfigurationManager.AppSettings["bootstrapUrl"])
                }
            };
            using (var cluster = new Cluster(configuration))
            {
                var clusterManager = cluster.CreateManager("Administrator", "password");

                var result = clusterManager.CreateBucket("test1");
                Assert.IsNullOrEmpty(result.Message);
                Assert.IsTrue(result.Success);
            }
        }

        [Test]
        public void Test_RemoveBucket()
        {
            var configuration = new ClientConfiguration
            {
                Servers = new List<Uri>
                {
                    new Uri(ConfigurationManager.AppSettings["bootstrapUrl"])
                }
            };
            using (var cluster = new Cluster(configuration))
            {
                var clusterManager = cluster.CreateManager("Administrator", "password");
                var result = clusterManager.RemoveBucket("test1");

                Assert.IsNullOrEmpty(result.Message);
                Assert.IsTrue(result.Success);
            }
        }

        [Test]
        public void Test_CreateBucketAsync()
        {
            var configuration = new ClientConfiguration
            {
                Servers = new List<Uri>
                {
                    new Uri(ConfigurationManager.AppSettings["bootstrapUrl"])
                }
            };
            using (var cluster = new Cluster(configuration))
            {
                var clusterManager = cluster.CreateManager("Administrator", "password");
                var result = clusterManager.CreateBucketAsync("test").Result;

                Assert.IsNullOrEmpty(result.Message);
                Assert.IsTrue(result.Success);
            }
        }

        [Test]
        public void Test_RemoveBucketAsync()
        {
            var configuration = new ClientConfiguration
            {
                Servers = new List<Uri>
                {
                    new Uri(ConfigurationManager.AppSettings["bootstrapUrl"])
                }
            };
            using (var cluster = new Cluster(configuration))
            {
                var clusterManager = cluster.CreateManager("Administrator", "password");
                var result = clusterManager.RemoveBucketAsync("test").Result;

                Assert.IsNullOrEmpty(result.Message);
                Assert.IsTrue(result.Success);
            }
        }
    }
}
