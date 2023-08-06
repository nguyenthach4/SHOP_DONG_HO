var contact = {
    init: function () {
        contact.registerEvent();
    },
    registerEvent: function () {
        contact.initMap();
    },
    initMap: function () {
        const lat = $('#hidLat').val().replace(/\,/g, '.');
        const lng = $('#hidLng').val().replace(/\,/g, '.');
        const uluru = { lat: parseFloat(lat), lng: parseFloat(lng) };
        const map = new google.maps.Map(document.getElementById("map"), {
            zoom: 17,
            center: uluru,
        });
      //  console.log(uluru);
        const contentString = $('#hidAddress').val();
        const infowindow = new google.maps.InfoWindow({
            content: contentString,
        });
        const marker = new google.maps.Marker({
            position: uluru,
            map,
            title: $('#hidName').val()
        });
        infowindow.open(map, marker);
    }

}
contact.init();