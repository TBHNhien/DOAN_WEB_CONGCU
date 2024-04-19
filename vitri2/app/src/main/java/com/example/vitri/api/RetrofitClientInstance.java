package com.example.vitri.api;

import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class RetrofitClientInstance {
    private static Retrofit retrofit;
    private static final String BASE_URL = "http://192.168.1.15:3000/";

//   private static final String BASE_URL = "http://10.0.2.2:3000/"; // Thay thế với URL cơ sở của bạn

//    private static final String BASE_URL = "http://10.14.101.117:3000/";

    public static Retrofit getRetrofitInstance() {
        if (retrofit == null) {
            retrofit = new retrofit2.Retrofit.Builder()
                    .baseUrl(BASE_URL)
                    .addConverterFactory(GsonConverterFactory.create())
                    .build();
        }
        return retrofit;
    }
}