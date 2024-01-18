CREATE TABLE "public.User" (
	"id" serial NOT NULL,
	"email" VARCHAR(200) NOT NULL,
	"password" VARCHAR(255) NOT NULL,
	"last_access" DATETIME NOT NULL,
	"active" BOOLEAN NOT NULL DEFAULT 'true',
	CONSTRAINT "User_pk" PRIMARY KEY ("id")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "public.Costs" (
	"id" serial NOT NULL,
	"title" VARCHAR(100) NOT NULL,
	"descripcion" TEXT NOT NULL,
	"value" FLOAT NOT NULL,
	"active" BOOLEAN NOT NULL DEFAULT 'true',
	"created_at" DATETIME NOT NULL DEFAULT 'now',
	"user_id_requested" integer NOT NULL,
	CONSTRAINT "Costs_pk" PRIMARY KEY ("id")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "public.cost_user" (
	"id" serial NOT NULL,
	"value" FLOAT NOT NULL,
	"payed" BOOLEAN NOT NULL DEFAULT 'false',
	"payed_at" DATETIME NOT NULL,
	"cost_id" integer NOT NULL,
	"user_id" integer NOT NULL,
	"created_at" DATETIME NOT NULL DEFAULT 'now',
	CONSTRAINT "cost_user_pk" PRIMARY KEY ("id")
) WITH (
  OIDS=FALSE
);




ALTER TABLE "Costs" ADD CONSTRAINT "Costs_fk0" FOREIGN KEY ("user_id_requested") REFERENCES "User"("id");

ALTER TABLE "cost_user" ADD CONSTRAINT "cost_user_fk0" FOREIGN KEY ("cost_id") REFERENCES "Costs"("id");
ALTER TABLE "cost_user" ADD CONSTRAINT "cost_user_fk1" FOREIGN KEY ("user_id") REFERENCES "User"("id");




