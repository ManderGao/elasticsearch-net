﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Start a machine learning datafeed.
		/// A datafeed must be started in order to retrieve data from Elasticsearch. A datafeed can be started and stopped multiple times throughout
		/// its lifecycle.
		/// </summary>
		IStartDatafeedResponse StartDatafeed(Id datafeedId, Func<StartDatafeedDescriptor, IStartDatafeedRequest> selector = null);

		/// <inheritdoc />
		IStartDatafeedResponse StartDatafeed(IStartDatafeedRequest request);

		/// <inheritdoc />
		Task<IStartDatafeedResponse> StartDatafeedAsync(Id datafeedId, Func<StartDatafeedDescriptor, IStartDatafeedRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc />
		Task<IStartDatafeedResponse> StartDatafeedAsync(IStartDatafeedRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IStartDatafeedResponse StartDatafeed(Id datafeedId, Func<StartDatafeedDescriptor, IStartDatafeedRequest> selector = null) =>
			StartDatafeed(selector.InvokeOrDefault(new StartDatafeedDescriptor(datafeedId)));

		/// <inheritdoc />
		public IStartDatafeedResponse StartDatafeed(IStartDatafeedRequest request) =>
			DoRequest<IStartDatafeedRequest, StartDatafeedResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IStartDatafeedResponse> StartDatafeedAsync(
			Id datafeedId,
			Func<StartDatafeedDescriptor, IStartDatafeedRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			StartDatafeedAsync(selector.InvokeOrDefault(new StartDatafeedDescriptor(datafeedId)), cancellationToken);

		/// <inheritdoc />
		public Task<IStartDatafeedResponse> StartDatafeedAsync(IStartDatafeedRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IStartDatafeedRequest, IStartDatafeedResponse, StartDatafeedResponse>
				(request, request.RequestParameters, ct);
	}
}