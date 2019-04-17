﻿using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Cluster.NodesInfo
{
	public class NodesInfoApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, NodesInfoResponse, INodesInfoRequest, NodesInfoDescriptor, NodesInfoRequest>
	{
		public NodesInfoApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_nodes";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.NodesInfo(),
			(client, f) => client.NodesInfoAsync(),
			(client, r) => client.NodesInfo(r),
			(client, r) => client.NodesInfoAsync(r)
		);

		protected override void ExpectResponse(NodesInfoResponse response)
		{
			response.ClusterName.Should().NotBeNullOrWhiteSpace();
			Assert(response.NodeStatistics);
			response.Nodes.Should().NotBeEmpty().And.HaveCount(1);
			var kv = response.Nodes.FirstOrDefault();
			kv.Key.Should().NotBeNullOrWhiteSpace();
			var node = kv.Value;
			Assert(node);
			Assert(node.OperatingSystem);
			Assert(node.Plugins);
			Assert(node.Process);
			Assert(node.Jvm);
			Assert(node.ThreadPool);
			Assert(node.Transport);
			Assert(node.Http);
		}

		private static void Assert(NodeStatistics nodesMetada)
		{
			nodesMetada.Should().NotBeNull();
			nodesMetada.Total.Should().BeGreaterThan(0);
			nodesMetada.Successful.Should().BeGreaterThan(0);
		}

		protected void Assert(Nest.NodeInfo node)
		{
			node.Should().NotBeNull();
			node.Name.Should().NotBeNullOrWhiteSpace();
			node.TransportAddress.Should().NotBeNullOrWhiteSpace();
			node.Host.Should().NotBeNullOrWhiteSpace();
			node.Ip.Should().NotBeNullOrWhiteSpace();
			node.Version.Should().NotBeNullOrWhiteSpace();
			node.BuildHash.Should().NotBeNullOrWhiteSpace();
			node.Roles.Should().NotBeNullOrEmpty();
		}

		protected void Assert(NodeOperatingSystemInfo os)
		{
			os.Should().NotBeNull();
			os.RefreshInterval.Should().Be(1000);
			os.AvailableProcessors.Should().BeGreaterThan(0);
			os.Name.Should().NotBeNullOrWhiteSpace();
			os.Architecture.Should().NotBeNullOrWhiteSpace();
			os.Version.Should().NotBeNullOrWhiteSpace();
		}

		protected void Assert(List<PluginStats> plugins)
		{
			plugins.Should().NotBeEmpty();
			var plugin = plugins.First();
			plugin.Name.Should().NotBeNullOrWhiteSpace();
			plugin.Description.Should().NotBeNullOrWhiteSpace();
			plugin.Version.Should().NotBeNullOrWhiteSpace();
			plugin.ClassName.Should().NotBeNullOrWhiteSpace();
		}

		protected void Assert(NodeProcessInfo process)
		{
			process.Id.Should().BeGreaterThan(0);
			process.RefreshIntervalInMilliseconds.Should().BeGreaterThan(0);
			process.MlockAll.Should().Be(false);
		}

		protected void Assert(NodeJvmInfo jvm)
		{
			jvm.Should().NotBeNull();
			jvm.PID.Should().BeGreaterThan(0);
			jvm.StartTime.Should().BeGreaterThan(0);
			jvm.Version.Should().NotBeNullOrWhiteSpace();
			jvm.VMName.Should().NotBeNullOrWhiteSpace();
			jvm.VMVendor.Should().NotBeNullOrWhiteSpace();
			jvm.VMVersion.Should().NotBeNullOrWhiteSpace();
			jvm.GCCollectors.Should().NotBeEmpty();
			jvm.MemoryPools.Should().NotBeEmpty();
			jvm.Memory.Should().NotBeNull();
			jvm.Memory.DirectMaxInBytes.Should().BeGreaterOrEqualTo(0);
			jvm.Memory.NonHeapMaxInBytes.Should().BeGreaterOrEqualTo(0);
			jvm.Memory.NonHeapInitInBytes.Should().BeGreaterThan(0);
			jvm.Memory.HeapMaxInBytes.Should().BeGreaterThan(0);
			jvm.Memory.HeapInitInBytes.Should().BeGreaterThan(0);
		}

		protected void Assert(Dictionary<string, NodeThreadPoolInfo> pools)
		{
			pools.Should().NotBeEmpty().And.ContainKey("fetch_shard_store");
			var pool = pools["fetch_shard_store"];
			pool.KeepAlive.Should().NotBeNullOrWhiteSpace();
			pool.Type.Should().Be("scaling");
			pool.QueueSize.Should().BeGreaterOrEqualTo(-1);
		}

		protected void Assert(NodeInfoTransport transport)
		{
			transport.Should().NotBeNull();
			transport.BoundAddress.Should().NotBeEmpty();
			transport.PublishAddress.Should().NotBeNullOrWhiteSpace();
		}

		protected void Assert(Nest.NodeInfoHttp http)
		{
			http.Should().NotBeNull();
			http.BoundAddress.Should().NotBeEmpty();
			http.PublishAddress.Should().NotBeNullOrWhiteSpace();
		}
	}

	public class NodesInfoMissingNodeApiTests : NodesInfoApiTests
	{
		private static readonly NodeIds Nodes = NodeIds.Parse("_local,x");

		public NodesInfoMissingNodeApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override Func<NodesInfoDescriptor, INodesInfoRequest> Fluent => n => n.NodeId(Nodes);
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override NodesInfoRequest Initializer => new NodesInfoRequest(Nodes);
		protected override string UrlPath => "/_nodes/_local%2Cx";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.NodesInfo(f),
			(client, f) => client.NodesInfoAsync(f),
			(client, r) => client.NodesInfo(r),
			(client, r) => client.NodesInfoAsync(r)
		);

		protected override void ExpectResponse(NodesInfoResponse response)
		{
			response.ClusterName.Should().NotBeNullOrWhiteSpace();
			Assert(response.NodeStatistics);
			response.Nodes.Should().NotBeEmpty().And.HaveCount(1);
			var kv = response.Nodes.FirstOrDefault();
			kv.Key.Should().NotBeNullOrWhiteSpace();
			var node = kv.Value;
			Assert(node);
			Assert(node.OperatingSystem);
			Assert(node.Plugins);
			Assert(node.Process);
			Assert(node.Jvm);
			Assert(node.ThreadPool);
			Assert(node.Transport);
			Assert(node.Http);
		}

		private static void Assert(NodeStatistics nodesMetada)
		{
			nodesMetada.Should().NotBeNull();
			nodesMetada.Total.Should().Be(1);
			nodesMetada.Successful.Should().Be(1);
		}
	}
}
