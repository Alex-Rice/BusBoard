$(document).ready(function() {
    num = 1;
    image = document.getElementById("slideshow");
    imglink = document.getElementById("imglink");
    imgTitle = document.getElementById("imgTitle");

    setInterval(updatePage, 5000);

    function updatePage() {
        if (num == 4) {
            num = 0;
            reloadTimes();
        }
        switch (num) {
        case 0:
            source = "Images/bus.jpg";
            imageTitle = "55 Buses, Clapton Pond by oxyman. CC BY-SA 2.0";
            imagelink =
                "https://en.wikipedia.org/wiki/Bus_bunching#/media/File:London_Bus_route_55_Buses,_Clapton_Pond.jpg";
            break;
        case 1:
            source = "Images/bus1.jpg";
            imageTitle = "Photo: George Fox Evangelical Seminary via Flickr. Licenced for use under Creative Commons.";
            imagelink =
                "https://www.flickr.com/photos/gfes/10233757895/in/photolist-gAjFFD-bnccF8-gPvMbe-7m5Kj-pLDBjp-8WuNsv-bbKz8i-65jCn5-2cGyEe-bB4NUf-7m5zZ-kjP28k-dtzFjd-gPxzLY-8dmrEK-5BjhRi-ac6rLP-aHXbvF-4V7Wvn-9WpR2e-qBP3wo-8z5yZJ-7m5sL-h5PynH-pgGQdT-BNP8J-oaqKcv-2bzvRv-4ngSXZ-qhL5ML-ooo6z4-4ArRfs-muqV4x-wpsKg-oGCEek-gPxYrC-5xxMie-51efJy-4AsQ99-MJXQ-5hwxoN-gPzLfG-8oBJXP-bbQCcT-219aj-9W56TP-gPyhzx-AhEgW-Eea2h-5ytjkF";
            break;
        case 2:
            source = "Images/bus2.jpg";
            imageTitle = "A Stagecoach bus on London Bridge Martin Arrand/Creative Commons licence CC BY-ND 2.0";
            imagelink =
                "http://www.standard.co.uk/news/transport/thousands-of-london-buses-set-to-run-on-meat-waste-from-2016-a3141886.html";
            break;
        case 3:
            source = "Images/bus3.png";
            imageTitle = "A beautiful hand crochet London bus rattle.";
            imagelink = "https://www.ltmuseumshop.co.uk/toys/hand-crochet-london-bus-rattle";
            break;
        }
        image.src = source;
        imglink.href = imagelink;
        imgTitle.title = imageTitle;

        num += 1;

    }

    $("#PostCodeForm").submit(function(event) {
        event.preventDefault();
        newPostCode = $('input:first').val();
        if (newPostCode) {
            javaPostCode = newPostCode.replace(" ", "+");
            reloadTimes();
        }
    });

    function reloadTimes() {
        console.log(javaPostCode);
        $.ajax("/Home/BusTimes?Postcode=" + javaPostCode,
            {
                success: function (data) {
                    $('#updateDiv').html($(data));
                },
                error: function () {
                    console.log("error");
                }
            });
    }
});