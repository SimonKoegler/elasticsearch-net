using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.GetSource
{
	public partial class GetSourceTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class RealtimeTests : YamlTestsBase
		{
			[Test]
			public void RealtimeTest()
			{	

				//skip 0 - 0.90.0; 
				this.Skip("0 - 0.90.0", "Get source not supported in pre 0.90.1 versions.");

				//do indices.create 
				_body = new {
					settings= new {
						refresh_interval= "-1",
						number_of_replicas= "0"
					}
				};
				this.Do(()=> this._client.IndicesCreatePost("test_1", _body));

				//do cluster.health 
				this.Do(()=> this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status", @"green")
				));

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> this._client.IndexPost("test_1", "test", "1", _body));

				//do get_source 
				this.Do(()=> this._client.GetSource("test_1", "test", "1", nv=>nv
					.Add("realtime", 1)
				));

				//match this._status: 
				this.IsMatch(this._status, new {
					foo= "bar"
				});

				//do get_source 
				this.Do(()=> this._client.GetSource("test_1", "test", "1", nv=>nv
					.Add("realtime", 0)
				));

				//do get_source 
				this.Do(()=> this._client.GetSource("test_1", "test", "1", nv=>nv
					.Add("realtime", 0)
					.Add("refresh", 1)
				));

				//match this._status: 
				this.IsMatch(this._status, new {
					foo= "bar"
				});

			}
		}
	}
}

