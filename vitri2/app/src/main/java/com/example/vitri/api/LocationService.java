package com.example.vitri.api;

import java.util.List;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.POST;

public interface LocationService {
    @POST("/api/location")
    Call<Void> postLocation(@Body LocationModel location);

    @GET("/api/history")
    Call<List<LocationModel>> getHistory();
}

