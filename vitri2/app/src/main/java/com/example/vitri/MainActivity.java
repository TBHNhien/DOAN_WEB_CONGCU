package com.example.vitri;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.app.ActivityCompat;

import android.content.pm.PackageManager;
import android.graphics.Color;
import android.os.AsyncTask;
import android.os.Bundle;
import android.Manifest;
import android.location.Location;
import android.os.Looper;
import android.util.Log;
import android.view.View;
import android.widget.Button;


import com.example.vitri.api.LocationModel;
import com.example.vitri.api.LocationService;
import com.example.vitri.api.RetrofitClientInstance;
import com.google.android.gms.location.FusedLocationProviderClient;
import com.google.android.gms.location.LocationCallback;
import com.google.android.gms.location.LocationRequest;
import com.google.android.gms.location.LocationResult;
import com.google.android.gms.location.LocationServices;
import com.google.android.gms.tasks.OnSuccessListener;

import org.osmdroid.api.IMapController;
import org.osmdroid.config.Configuration;
import org.osmdroid.library.BuildConfig;
import org.osmdroid.tileprovider.tilesource.TileSourceFactory;
import org.osmdroid.util.BoundingBox;
import org.osmdroid.util.GeoPoint;
import org.osmdroid.views.MapView;
import org.osmdroid.views.overlay.Marker;
import org.osmdroid.views.overlay.Polyline;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
public class MainActivity extends AppCompatActivity {
    private static final int YOUR_PERMISSIONS_REQUEST_CODE = 1;
    private FusedLocationProviderClient fusedLocationClient;

    private MapView map; // Biến toàn cục để tham chiếu đến MapView
    private IMapController mapController; // Biến toàn cục để tham chiếu đến controller của MapView


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);


        // Cấu hình khi sử dụng Osmdroid trong ứng dụng của bạn
        Configuration.getInstance().setUserAgentValue(BuildConfig.APPLICATION_ID);

        map = (MapView) findViewById(R.id.map);
        map.setTileSource(TileSourceFactory.MAPNIK);

        mapController = map.getController();
        mapController.setZoom(18);
        // Bạn có thể thiết lập vị trí mặc định nếu muốn
        GeoPoint startPoint = new GeoPoint(37.4219983, -122.084); // Ví dụ: Vị trí của tháp Eiffel
        mapController.setCenter(startPoint);

        fusedLocationClient = LocationServices.getFusedLocationProviderClient(this);
        //getLocation();

        Button btnLocation = findViewById(R.id.button_location);
        btnLocation.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                // Khi nút được nhấn, gọi phương thức getLocation để lấy vị trí hiện tại
                getLocation();
            }
        });
    }




    private void getLocation() {
        if (ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            ActivityCompat.requestPermissions(this, new String[]{Manifest.permission.ACCESS_FINE_LOCATION, Manifest.permission.ACCESS_COARSE_LOCATION}, YOUR_PERMISSIONS_REQUEST_CODE);
            return;
        }
        // Tạo một yêu cầu vị trí
        LocationRequest locationRequest = LocationRequest.create();
        locationRequest.setPriority(LocationRequest.PRIORITY_HIGH_ACCURACY);
        locationRequest.setInterval(100000); // Cập nhật vị trí mỗi 100 giây

        // Bắt đầu cập nhật vị trí
        fusedLocationClient.requestLocationUpdates(locationRequest,
                new LocationCallback() {
                    public void onLocationResult(LocationResult locationResult) {
                        if (locationResult == null) {
                            return;
                        }
                        Location location = locationResult.getLastLocation();
                        // Sử dụng vị trí ở đây
                        GeoPoint currentLocation = new GeoPoint(location.getLatitude(), location.getLongitude());
                        mapController.setCenter(currentLocation);

                        Marker startMarker = new Marker(map);
                        startMarker.setPosition(currentLocation);
                        startMarker.setAnchor(Marker.ANCHOR_CENTER, Marker.ANCHOR_BOTTOM);
                        startMarker.setTitle("Vị trí hiện tại");

                        map.getOverlays().clear();
                        map.getOverlays().add(startMarker);
                        map.invalidate();

                        // Đoạn code mới để gửi thông tin vị trí lên máy chủ
                        LocationService service = RetrofitClientInstance.getRetrofitInstance().create(LocationService.class);
                        LocationModel locationModel = new LocationModel(location.getLatitude(), location.getLongitude());
                        Call<Void> call = service.postLocation(locationModel);
                        call.enqueue(new Callback<Void>() {
                            @Override
                            public void onResponse(Call<Void> call, Response<Void> response) {
                                if (response.isSuccessful()) {
                                    // Xử lý khi gửi dữ liệu thành công
                                    Log.d("Location Update", "Location updated successfully!");
                                }
                            }

                            @Override
                            public void onFailure(Call<Void> call, Throwable t) {
                                // Xử lý khi gửi dữ liệu thất bại
                                Log.e("Location Update Error", t.getMessage());
                            }
                        });
                    }
                },
                Looper.getMainLooper()); // Sử dụng main looper để cập nhật UI
    }





    @Override
    public void onRequestPermissionsResult(int requestCode, @NonNull String[] permissions, @NonNull int[] grantResults) {
        super.onRequestPermissionsResult(requestCode, permissions, grantResults);
        if (requestCode == YOUR_PERMISSIONS_REQUEST_CODE) {
            if (grantResults.length > 0 && grantResults[0] == PackageManager.PERMISSION_GRANTED) {
                getLocation(); // Gọi lại getLocation() vì quyền đã được cấp
            } else {
                // Quyền bị từ chối, thông báo cho người dùng
            }
        }
    }



    public void onViewHistoryClicked(View view) {
        // Khởi tạo Retrofit instance nếu chưa có
        LocationService service = RetrofitClientInstance.getRetrofitInstance().create(LocationService.class);
        // Gọi API để lấy lịch sử vị trí
        Call<List<LocationModel>> call = service.getHistory();

        call.enqueue(new Callback<List<LocationModel>>() {
            @Override
            public void onResponse(Call<List<LocationModel>> call, Response<List<LocationModel>> response) {
                if (response.isSuccessful() && response.body() != null) {
                    List<LocationModel> historyLocations = response.body();
                    // Cập nhật bản đồ với lịch sử vị trí
                    updateMapWithHistoryLocations(historyLocations);
                    Log.e("ViewHistory", "Response  successful");
                } else {
                    // Xử lý khi phản hồi không thành công
                    Log.e("ViewHistory", "Response not successful");
                }
            }

            @Override
            public void onFailure(Call<List<LocationModel>> call, Throwable t) {
                // Xử lý khi có lỗi xảy ra trong quá trình gọi API
                Log.e("ViewHistory", "API call failed: " + t.getMessage());
            }
        });
    }

    // Đoạn này giữ nguyên, thêm vào lớp chứa phương thức updateMapWithHistoryLocations của bạn
    private BoundingBox getBoundingBox(List<LocationModel> locations) {
        double minLat = Double.MAX_VALUE;
        double minLon = Double.MAX_VALUE;
        double maxLat = -Double.MAX_VALUE;
        double maxLon = -Double.MAX_VALUE;

        for (LocationModel location : locations) {
            minLat = Math.min(location.getLatitude(), minLat);
            minLon = Math.min(location.getLongitude(), minLon);
            maxLat = Math.max(location.getLatitude(), maxLat);
            maxLon = Math.max(location.getLongitude(), maxLon);
        }

        return new BoundingBox(maxLat, maxLon, minLat, minLon);
    }

    // Phương thức updateMapWithHistoryLocations được cập nhật để bao gồm zoomToBoundingBox
    private void updateMapWithHistoryLocations(List<LocationModel> historyLocations) {
        map.getOverlays().clear();
        for (LocationModel locationModel : historyLocations) {
            GeoPoint point = new GeoPoint(locationModel.getLatitude(), locationModel.getLongitude());
            Marker marker = new Marker(map);
            marker.setPosition(point);
            marker.setAnchor(Marker.ANCHOR_CENTER, Marker.ANCHOR_BOTTOM);
            map.getOverlays().add(marker);
        }

        // Nếu có ít nhất một vị trí, điều chỉnh bản đồ để hiển thị tất cả các markers
        if (!historyLocations.isEmpty()) {
            BoundingBox boundingBox = getBoundingBox(historyLocations);
            map.zoomToBoundingBox(boundingBox, true); // Hoặc mapController.setCenter() và mapController.zoomTo()
        }

        map.invalidate(); // Refresh bản đồ để cập nhật các thay đổi
    }
}