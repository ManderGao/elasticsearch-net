﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Checks the status of a trial license.
		/// </summary>
		/// <remarks>
		/// Valid in Elasticsearch 6.2.0+.
		/// </remarks>
		IGetTrialLicenseStatusResponse GetTrialLicenseStatus(Func<GetTrialLicenseStatusDescriptor, IGetTrialLicenseStatusRequest> selector = null);

		/// <summary>
		/// Checks the status of a trial license.
		/// </summary>
		/// <remarks>
		/// Valid in Elasticsearch 6.2.0+.
		/// </remarks>
		IGetTrialLicenseStatusResponse GetTrialLicenseStatus(IGetTrialLicenseStatusRequest request);

		/// <summary>
		/// Checks the status of a trial license.
		/// </summary>
		/// <remarks>
		/// Valid in Elasticsearch 6.2.0+.
		/// </remarks>
		Task<IGetTrialLicenseStatusResponse> GetTrialLicenseStatusAsync(
			Func<GetTrialLicenseStatusDescriptor, IGetTrialLicenseStatusRequest> selector = null,
			CancellationToken ct = default
		);

		/// <summary>
		/// Checks the status of a trial license.
		/// </summary>
		/// <remarks>
		/// Valid in Elasticsearch 6.2.0+.
		/// </remarks>
		Task<IGetTrialLicenseStatusResponse> GetTrialLicenseStatusAsync(IGetTrialLicenseStatusRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetTrialLicenseStatusResponse GetTrialLicenseStatus(
			Func<GetTrialLicenseStatusDescriptor, IGetTrialLicenseStatusRequest> selector = null
		) =>
			GetTrialLicenseStatus(selector.InvokeOrDefault(new GetTrialLicenseStatusDescriptor()));

		/// <inheritdoc />
		public IGetTrialLicenseStatusResponse GetTrialLicenseStatus(IGetTrialLicenseStatusRequest request) =>
			DoRequest<IGetTrialLicenseStatusRequest, GetTrialLicenseStatusResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IGetTrialLicenseStatusResponse> GetTrialLicenseStatusAsync(
			Func<GetTrialLicenseStatusDescriptor, IGetTrialLicenseStatusRequest> selector = null,
			CancellationToken ct = default
		) =>
			GetTrialLicenseStatusAsync(selector.InvokeOrDefault(new GetTrialLicenseStatusDescriptor()), ct);

		/// <inheritdoc />
		public Task<IGetTrialLicenseStatusResponse> GetTrialLicenseStatusAsync(IGetTrialLicenseStatusRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetTrialLicenseStatusRequest, IGetTrialLicenseStatusResponse, GetTrialLicenseStatusResponse>(request, request.RequestParameters, ct);
	}
}