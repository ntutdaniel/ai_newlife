from imgurpython import ImgurClient

def upload_photo(image_path):
    client_id = 'e485f2b4b44b370'
    client_secret = '20f3519fcea96fe96df44c00f1fbe715b0b46567'
    access_token = '8087e735177d3840aef4be612b580a7e88134312'
    refresh_token = '78d7eb1164879679ba72cc5614e2ea5824c6d8e7'
    client = ImgurClient(client_id, client_secret, access_token, refresh_token)
    album = None # You can also enter an album ID here
    config = {
        'album': album
    }

    print("Uploading image... ")
    image = client.upload_from_path(image_path, config=config, anon=False)
    print("Done")

    return image['link']