Elasticsearch & Kibana Docker Setup
This guide will help you set up Elasticsearch and Kibana using Docker.

How Does It Work?
1. Prerequisites
Docker: Make sure you have Docker installed on your machine. If not, download and install Docker before proceeding.
2. Pull Docker Images
First, open your terminal and pull the necessary Docker images.

Elasticsearch: Run the following command to pull Elasticsearch:

bash
docker pull elasticsearch:8.15.0
Kibana (optional): If you want to monitor Elasticsearch with Kibana, pull the Kibana image as well:

bash
docker pull kibana:8.15.0

3. Create a docker-compose.yaml File
Now, you are ready to set up Elasticsearch and Kibana using Docker Compose. Create a docker-compose.yaml file in your project directory with the following content:

yaml
version: '3.8'

services:
  elasticsearch:
    image: elasticsearch:8.15.0
    environment:
      - xpack.security.enabled=false
      - "discovery.type=single-node"
    ports:
      - 9200:9200
    volumes:
      - elasticsearch-data:/usr/share/elasticsearch/data

  kibana:
    image: kibana:8.15.0
    ports:
      - 5601:5601
    environment:
      - ELASTICSEARCH_HOST=http://elasticsearch:9200

volumes:
  elasticsearch-data:
    driver: local
    
4. Start the Services
To start Elasticsearch and Kibana, run the following command in the directory where your docker-compose.yaml file is located:

bash
docker-compose up
This command will launch both Elasticsearch and Kibana, making them accessible via:

Elasticsearch: http://localhost:9200
Kibana: http://localhost:5601 (if Kibana is enabled)

5. Accessing Elasticsearch and Kibana
Once the containers are up and running:

You can access Elasticsearch at http://localhost:9200.
If you've set up Kibana, you can access it at http://localhost:5601 to monitor your Elasticsearch instance.

6. Stopping the Services
To stop the services, simply run:

bash
docker-compose down
This will stop and remove the containers, but the data will persist in the elasticsearch-data volume.

Enjoy your setup! If you encounter any issues, feel free to check the official Elasticsearch and Kibana documentation for further guidance.
