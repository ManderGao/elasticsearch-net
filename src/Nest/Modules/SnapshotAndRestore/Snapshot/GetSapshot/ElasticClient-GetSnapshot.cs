﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Gets information about one or more snapshots
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/modules-snapshots.html#_snapshot
		/// </summary>
		/// <param name="repository">The repository name under which the snapshots live</param>
		/// <param name="snapshots">The names of the snapshots we want information from (can be _all or wildcards)</param>
		/// <param name="selector">Optionally further describe the get snapshot operation</param>
		IGetSnapshotResponse GetSnapshot(Name repository, Names snapshots, Func<GetSnapshotDescriptor, IGetSnapshotRequest> selector = null);

		/// <inheritdoc />
		IGetSnapshotResponse GetSnapshot(IGetSnapshotRequest request);

		/// <inheritdoc />
		Task<IGetSnapshotResponse> GetSnapshotAsync(Name repository, Names snapshots,
			Func<GetSnapshotDescriptor, IGetSnapshotRequest> selector = null, CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IGetSnapshotResponse> GetSnapshotAsync(IGetSnapshotRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetSnapshotResponse GetSnapshot(Name repository, Names snapshots, Func<GetSnapshotDescriptor, IGetSnapshotRequest> selector = null) =>
			GetSnapshot(selector.InvokeOrDefault(new GetSnapshotDescriptor(repository, snapshots)));

		/// <inheritdoc />
		public IGetSnapshotResponse GetSnapshot(IGetSnapshotRequest request) =>
			DoRequest<IGetSnapshotRequest, GetSnapshotResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IGetSnapshotResponse> GetSnapshotAsync(
			Name repository,
			Names snapshots,
			Func<GetSnapshotDescriptor, IGetSnapshotRequest> selector = null,
			CancellationToken ct = default
		) =>
			GetSnapshotAsync(selector.InvokeOrDefault(new GetSnapshotDescriptor(repository, snapshots)), ct);

		/// <inheritdoc />
		public Task<IGetSnapshotResponse> GetSnapshotAsync(IGetSnapshotRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetSnapshotRequest, IGetSnapshotResponse, GetSnapshotResponse>(request, request.RequestParameters, ct);
	}
}