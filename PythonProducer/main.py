import json
from pathlib import Path
from confluent_kafka import Producer

config  = {
    "bootstrap.servers":"localhost:9092"
    }

producer = Producer(config)

path = "data/Fild_report.json"

with open(path, "r", encoding="utf-8") as f:
    all_data = json.load(f)

count = 0
for data in all_data:

    try:

        message = json.dumps(data, ensure_ascii=False)

        producer.produce("InvalidData",
                        value= message)

        count += 1

        print(f"add data :{count}")
    except Exception as e:
        print(f"Eror:{e}")

producer.flush()


